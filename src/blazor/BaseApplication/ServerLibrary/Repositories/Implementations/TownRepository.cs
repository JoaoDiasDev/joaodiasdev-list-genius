namespace ServerLibrary.Repositories.Implementations;

public class TownRepository(AppDbContext appDbContext) : IGenericRepository<Town>
{
    public async Task<List<Town>> GetAll()
    {
        return await appDbContext.Towns.ToListAsync();
    }

    public async Task<Town> GetById(int id)
    {
        return (await appDbContext.Towns.FindAsync(id))!;
    }

    public async Task<GeneralResponse> Create(Town item)
    {
        var checkIfNull = await CheckName(item.Name);
        if (!checkIfNull) return AlreadyExist();
        appDbContext.Towns.Add(item);
        await Commit();
        return Success();
    }

    public async Task<GeneralResponse> Update(Town item)
    {
        var dep = await appDbContext.Towns.FindAsync(item.Id);
        if (dep is null) return NotFound();
        dep.Name = item.Name;
        await Commit();
        return Success();
    }

    public async Task<GeneralResponse> DeleteById(int id)
    {
        var dep = await appDbContext.Towns.FindAsync(id);
        if (dep is null) return NotFound();

        appDbContext.Towns.Remove(dep);
        await Commit();
        return Success();
    }

    private static GeneralResponse NotFound() => new(false, "Town not found");
    private static GeneralResponse Success() => new(true, "Process completed");
    private static GeneralResponse AlreadyExist() => new(false, "Town already exist");
    private async Task Commit() => await appDbContext.SaveChangesAsync();

    private async Task<bool> CheckName(string name)
    {
        var item = await appDbContext.Towns
            .FirstOrDefaultAsync(x => x.Name.ToLower().Equals(name.ToLower()));
        return item is null;
    }
}