using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;

namespace WebApplication1.Services;

public interface IMainService
{
    Task<List<Staff>> GetStaffs(int? roleId);
    Task<Staff?> GetStaff(int staffId);
    Task CreateStaff(Staff staff);
    Task UpdateStaff(Staff staff);
    Task DeleteStaff(Staff staff);
}

public class MainService : IMainService
{
    private readonly ApplicationDbContext _applicationDbContext;

    public MainService(ApplicationDbContext applicationDbContext)
    {
        _applicationDbContext = applicationDbContext;
    }

    public async Task<List<Staff>> GetStaffs(int? roleId)
    {
        if (roleId == null)
        {
            return await _applicationDbContext.Staffs.Include(x=>x.StaffRole).ToListAsync();
        }

        return await _applicationDbContext.Staffs.Where(x => x.StaffRole.RoleId == roleId.Value).Include(x=>x.StaffRole).ToListAsync();
    }

    public async Task<Staff?> GetStaff(int staffId)
    {
        return await _applicationDbContext.Staffs.Where(x => x.StaffId == staffId).Include(x=>x.StaffRole).FirstOrDefaultAsync();
    }
    
    public async Task CreateStaff(Staff staff)
    {
        _applicationDbContext.Staffs.Add(staff);
        await _applicationDbContext.SaveChangesAsync();
    }
    
    public async Task UpdateStaff(Staff staff)
    {
        _applicationDbContext.Staffs.Update(staff);
        await _applicationDbContext.SaveChangesAsync();
    }
    
    public async Task DeleteStaff(Staff staff)
    {
        _applicationDbContext.Staffs.Remove(staff);
        await _applicationDbContext.SaveChangesAsync();
    }
    
}