namespace ServerLibrary.Repositories.Implementations
{
    public class VacationTypeRepository(AppDbContext appDbContext) : IGenericRepository<VacationType>
    {
        public async Task<GeneralResponse> DeleteById(int id)
        {
            var item = await appDbContext.VacationTypes.FindAsync(id);
            if (item is null) return NotFound();

            appDbContext.VacationTypes.Remove(item);
            await Commit();
            return Success();
        }

        public async Task<List<VacationType>> GetAll() => await appDbContext
            .VacationTypes
            .AsNoTracking()
            .ToListAsync();

        public async Task<VacationType> GetById(int id) => (await appDbContext
            .VacationTypes
            .FindAsync(id))!;

        public async Task<GeneralResponse> Create(VacationType item)
        {
            if (!await CheckName(item.Name)!) return new GeneralResponse(false, "Vacation type already added");
            appDbContext.VacationTypes.Add(item);
            await Commit();
            return Success();
        }

        public async Task<GeneralResponse> Update(VacationType item)
        {
            var obj = await appDbContext.VacationTypes.FindAsync(item.Id);
            if (obj is null) return NotFound();
            obj.Name = item.Name;
            await Commit();
            return Success();
        }

        private async Task<bool> CheckName(string name)
        {
            var item = await appDbContext
                .VacationTypes
                .FirstOrDefaultAsync(x => x.Name.ToLower().Equals(name.ToLower()));
            return item is null;
        }

        private async Task Commit() => await appDbContext.SaveChangesAsync();
        private static GeneralResponse NotFound() => new(false, "Sorry data not found");
        private static GeneralResponse Success() => new(true, "Success!");
    }

}
