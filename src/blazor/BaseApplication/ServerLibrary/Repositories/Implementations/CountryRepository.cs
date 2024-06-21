namespace ServerLibrary.Repositories.Implementations;

public class CountryRepository(AppDbContext appDbContext) : IGenericRepository<Country>
{
    public async Task<List<Country>> GetAll()
    {
        return await appDbContext.Countries.ToListAsync();
    }

    public async Task<Country> GetById(int id)
    {
        return (await appDbContext.Countries.FindAsync(id))!;
    }

    public async Task<GeneralResponse> Create(Country item)
    {
        var checkIfNull = await CheckName(item.Name);
        if (!checkIfNull) return AlreadyExist();
        appDbContext.Countries.Add(item);
        await Commit();
        return Success();
    }

    public async Task<GeneralResponse> Update(Country item)
    {
        var dep = await appDbContext.Countries.FindAsync(item.Id);
        if (dep is null) return NotFound();
        dep.Name = item.Name;
        await Commit();
        return Success();
    }

    public async Task<GeneralResponse> DeleteById(int id)
    {
        var dep = await appDbContext.Countries.FindAsync(id);
        if (dep is null) return NotFound();

        appDbContext.Countries.Remove(dep);
        await Commit();
        return Success();
    }

    private static GeneralResponse NotFound() => new(false, "Country not found");
    private static GeneralResponse Success() => new(true, "Process completed");
    private static GeneralResponse AlreadyExist() => new(false, "Country already exist");
    private async Task Commit() => await appDbContext.SaveChangesAsync();

    private async Task<bool> CheckName(string name)
    {
        var item = await appDbContext.Countries
            .FirstOrDefaultAsync(x => x.Name.ToLower().Equals(name.ToLower()));
        return item is null;
    }
}