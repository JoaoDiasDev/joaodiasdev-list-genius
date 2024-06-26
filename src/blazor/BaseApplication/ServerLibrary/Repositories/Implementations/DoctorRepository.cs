namespace ServerLibrary.Repositories.Implementations;

public class DoctorRepository(AppDbContext appDbContext) : IGenericRepository<Doctor>
{
    public async Task<GeneralResponse> DeleteById(int id)
    {
        var item = await appDbContext.Doctors.FirstOrDefaultAsync(eid => eid.EmployeeId == id);
        if (item is null) return NotFound();

        appDbContext.Doctors.Remove(item);
        await Commit();
        return Success();
    }

    public async Task<List<Doctor>> GetAll() => await appDbContext
        .Doctors
        .AsNoTracking()
        .ToListAsync();

    public async Task<Doctor> GetById(int id) => (await appDbContext
        .Doctors
        .FirstOrDefaultAsync(eid => eid.EmployeeId == id))!;

    public async Task<GeneralResponse> Create(Doctor item)
    {
        appDbContext.Doctors.Add(item);
        await Commit();
        return Success();
    }

    public async Task<GeneralResponse> Update(Doctor item)
    {
        var obj = await appDbContext.Doctors.FirstOrDefaultAsync(eid => eid.EmployeeId == item.EmployeeId);
        if (obj is null) return NotFound();
        obj.MedicalRecommendation = item.MedicalRecommendation;
        obj.MedicalDiagnose = item.MedicalDiagnose;
        obj.Date = item.Date;
        await Commit();
        return Success();
    }

    private async Task Commit() => await appDbContext.SaveChangesAsync();
    private static GeneralResponse NotFound() => new(false, "Sorry data not found");
    private static GeneralResponse Success() => new(true, "Success!");
}