namespace ServerLibrary.Repositories.Implementations;

public class CityRepository(AppDbContext appDbContext) : IGenericRepository<City>
{
    public async Task<List<City>> GetAll()
    {
        return await appDbContext.Cities.ToListAsync();
    }

    public async Task<City> GetById(int id)
    {
        return (await appDbContext.Cities.FindAsync(id))!;
    }

    public async Task<GeneralResponse> Create(City item)
    {
        if (!await CheckName(item.Name!)) return AlreadyExist();
        {
            appDbContext.Cities.Add(item);
            await Commit();
            return Success();
        }
    }

    public async Task<GeneralResponse> Update(City item)
    {
        var dep = await appDbContext.Cities.FindAsync(item.Id);
        if (dep is null) return NotFound();
        dep.Name = item.Name;
        await Commit();
        return Success();
    }

    public async Task<GeneralResponse> DeleteById(int id)
    {
        var dep = await appDbContext.Cities.FindAsync(id);
        if (dep is null) return NotFound();

        appDbContext.Cities.Remove(dep);
        await Commit();
        return Success();
    }

    private static GeneralResponse NotFound() => new(false, "City not found");
    private static GeneralResponse Success() => new(true, "Process completed");
    private static GeneralResponse AlreadyExist() => new(false, "City already exist");
    private async Task Commit() => await appDbContext.SaveChangesAsync();

    private async Task<bool> CheckName(string name)
    {
        var item = await appDbContext.Cities
            .FirstOrDefaultAsync(x => x.Name.ToLower().Equals(name.ToLower()));
        return item is null;
    }
}