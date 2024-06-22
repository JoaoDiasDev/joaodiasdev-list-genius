namespace ServerLibrary.Repositories.Implementations;

public class CityRepository(AppDbContext appDbContext) : IGenericRepository<City>
{
    public async Task<List<City>> GetAll()
    {
        return await appDbContext
            .Cities
            .AsNoTracking()
            .Include(c => c.Country)
            .ToListAsync();
    }

    public async Task<City> GetById(int id)
    {
        return (await appDbContext.Cities.FindAsync(id))!;
    }

    public async Task<GeneralResponse> Create(City item)
    {
        var checkIfNull = await CheckName(item.Name);
        if (!checkIfNull) return AlreadyExist();
        appDbContext.Cities.Add(item);
        await Commit();
        return Success();
    }

    public async Task<GeneralResponse> Update(City item)
    {
        var city = await appDbContext.Cities.FindAsync(item.Id);
        if (city is null) return NotFound();
        city.Name = item.Name;
        city.CountryId = item.CountryId;
        await Commit();
        return Success();
    }

    public async Task<GeneralResponse> DeleteById(int id)
    {
        var city = await appDbContext.Cities.FindAsync(id);
        if (city is null) return NotFound();
        appDbContext.Cities.Remove(city);
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