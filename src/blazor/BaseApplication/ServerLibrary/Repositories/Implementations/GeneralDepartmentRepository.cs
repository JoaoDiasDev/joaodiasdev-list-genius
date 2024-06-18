namespace ServerLibrary.Repositories.Implementations;

public class GeneralDepartmentRepository(AppDbContext appDbContext) : IGenericRepository<GeneralDepartment>
{
    public async Task<List<GeneralDepartment>> GetAll()
    {
        return await appDbContext.GeneralDepartments.ToListAsync();
    }

    public async Task<GeneralDepartment> GetById(int id)
    {
        return (await appDbContext.GeneralDepartments.FindAsync(id))!;
    }

    public async Task<GeneralResponse> Create(GeneralDepartment item)
    {
        if (!await CheckName(item.Name!)) return AlreadyExist();
        {
            appDbContext.GeneralDepartments.Add(item);
            await Commit();
            return Success();
        }
    }

    public async Task<GeneralResponse> Update(GeneralDepartment item)
    {
        var dep = await appDbContext.GeneralDepartments.FindAsync(item.Id);
        if (dep is null) return NotFound();
        dep.Name = item.Name;
        await Commit();
        return Success();
    }

    public async Task<GeneralResponse> DeleteById(int id)
    {
        var dep = await appDbContext.GeneralDepartments.FindAsync(id);
        if (dep is null) return NotFound();

        appDbContext.GeneralDepartments.Remove(dep);
        await Commit();
        return Success();
    }

    private static GeneralResponse NotFound() => new(false, "General Department not found");
    private static GeneralResponse Success() => new(true, "Process completed");
    private static GeneralResponse AlreadyExist() => new(false, "General Department already exist");
    private async Task Commit() => await appDbContext.SaveChangesAsync();

    private async Task<bool> CheckName(string name)
    {
        var item = await appDbContext.GeneralDepartments
            .FirstOrDefaultAsync(x => x.Name.ToLower().Equals(name.ToLower()));
        return item is null;
    }
}