namespace ServerLibrary.Repositories.Implementations;

public class BranchRepository(AppDbContext appDbContext) : IGenericRepository<Branch>
{
    public async Task<List<Branch>> GetAll()
    {
        return await appDbContext
            .Branches
            .AsNoTracking()
            .Include(d => d.Department)
            .ToListAsync();
    }

    public async Task<Branch> GetById(int id)
    {
        return (await appDbContext.Branches.FindAsync(id))!;
    }

    public async Task<GeneralResponse> Create(Branch item)
    {
        var checkIfNull = await CheckName(item.Name);
        if (!checkIfNull) return AlreadyExist();
        appDbContext.Branches.Add(item);
        await Commit();
        return Success();
    }

    public async Task<GeneralResponse> Update(Branch item)
    {
        var branch = await appDbContext.Branches.FindAsync(item.Id);
        if (branch is null) return NotFound();
        branch.Name = item.Name;
        branch.DepartmentId = item.DepartmentId;
        await Commit();
        return Success();
    }

    public async Task<GeneralResponse> DeleteById(int id)
    {
        var branch = await appDbContext.Branches.FindAsync(id);
        if (branch is null) return NotFound();

        appDbContext.Branches.Remove(branch);
        await Commit();
        return Success();
    }

    private static GeneralResponse NotFound() => new(false, "Branch not found");
    private static GeneralResponse Success() => new(true, "Process completed");
    private static GeneralResponse AlreadyExist() => new(false, "Branch already exist");
    private async Task Commit() => await appDbContext.SaveChangesAsync();

    private async Task<bool> CheckName(string name)
    {
        var item = await appDbContext.Branches
            .FirstOrDefaultAsync(x => x.Name.ToLower().Equals(name.ToLower()));
        return item is null;
    }
}