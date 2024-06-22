namespace ServerLibrary.Repositories.Implementations;

public class EmployeeRepository(AppDbContext appDbContext) : IGenericRepository<Employee>
{
    public async Task<GeneralResponse> DeleteById(int id)
    {
        var item = await appDbContext.Employees.FindAsync(id);
        if (item is null) return NotFound();

        appDbContext.Employees.Remove(item);
        await Commit();
        return Success();
    }

    public async Task<List<Employee>> GetAll()
    {
        var employees = await appDbContext.Employees
            .AsNoTracking()
            .Include(t => t.Town)
            .ThenInclude(c => c!.City)
            .ThenInclude(c => c!.Country)
            .Include(b => b.Branch)
            .ThenInclude(d => d!.Department)
            .ThenInclude(gd => gd!.GeneralDepartment)
            .ToListAsync();
        return employees;
    }

    public async Task<Employee> GetById(int id)
    {
        var employees = await appDbContext.Employees
            .Include(t => t.Town)
            .ThenInclude(c => c!.City)
            .ThenInclude(c => c!.Country)
            .Include(b => b.Branch)
            .ThenInclude(d => d!.Department)
            .ThenInclude(gd => gd!.GeneralDepartment)
            .FirstOrDefaultAsync(e => e.Id == id);
        return employees!;
    }

    public async Task<GeneralResponse> Create(Employee item)
    {
        if (!await CheckName(item.Name)!) return AlreadyExist(item);
        appDbContext.Employees.Add(item);
        await Commit();
        return Success();
    }

    public async Task<GeneralResponse> Update(Employee employee)
    {
        var existingEmployee = await appDbContext.Employees.FirstOrDefaultAsync(e => e.Id == employee.Id);
        if (existingEmployee is null) return new GeneralResponse(false, "Employee does not exist");

        existingEmployee.Name = employee.Name;
        existingEmployee.Other = employee.Other;
        existingEmployee.Address = employee.Address;
        existingEmployee.TelephoneNumber = employee.TelephoneNumber;
        existingEmployee.BranchId = employee.BranchId;
        existingEmployee.TownId = employee.TownId;
        existingEmployee.CivilId = employee.CivilId;
        existingEmployee.FileNumber = employee.FileNumber;
        existingEmployee.JobName = employee.JobName;
        existingEmployee.Photo = employee.Photo;

        await appDbContext.SaveChangesAsync();
        await Commit();
        return Success();
    }

    private static GeneralResponse NotFound() => new(false, "Employee not found");
    private static GeneralResponse Success() => new(true, "Process completed");
    private static GeneralResponse AlreadyExist(Employee employee) => new(false, $"{employee.Name} already exist");
    private async Task Commit() => await appDbContext.SaveChangesAsync();

    private async Task<bool> CheckName(string name)
    {
        var item = await appDbContext.Employees
            .FirstOrDefaultAsync(x => x.Name.ToLower().Equals(name.ToLower()));
        return item is null;
    }
}