namespace ServerLibrary.Repositories.Implementations
{
    public class BranchRepository(AppDbContext appDbContext) : IGenericRepository<Branch>
    {
        public async Task<List<Branch>> GetAll()
        {
            return await appDbContext.Branches.ToListAsync();
        }

        public async Task<Branch> GetById(int id)
        {
            return (await appDbContext.Branches.FindAsync(id))!;
        }

        public async Task<GeneralResponse> Create(Branch item)
        {
            if (!await CheckName(item.Name!)) return AlreadyExist();
            {
                appDbContext.Branches.Add(item);
                await Commit();
                return Success();
            }
        }

        public async Task<GeneralResponse> Update(Branch item)
        {
            var dep = await appDbContext.Branches.FindAsync(item.Id);
            if (dep is null) return NotFound();
            dep.Name = item.Name;
            await Commit();
            return Success();
        }

        public async Task<GeneralResponse> DeleteById(int id)
        {
            var dep = await appDbContext.Branches.FindAsync(id);
            if (dep is null) return NotFound();

            appDbContext.Branches.Remove(dep);
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
                .FirstOrDefaultAsync(x => x.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
            return item is null;
        }
    }

}
