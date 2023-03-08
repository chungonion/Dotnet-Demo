using HotChocolate.Execution;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;

namespace WebApplication1.GraphQL.Mutations;

[ExtendObjectType(Name = "Mutation")]
public class StaffMutation
{
    public async Task<Staff> UpdateStaffAsync(ApplicationDbContext dbContext, UpdateStaffInput input)
    {
        try
        {
            var staff = await dbContext.Staffs.FirstOrDefaultAsync(x=>x.StaffId == input.StaffId);
            staff.StaffName = input.StaffName;
            staff.RoleId = input.RoleId;
            dbContext.Staffs.Update(staff);
            await dbContext.SaveChangesAsync();
            return staff;
        }
        catch (Exception e)
        {
            throw new QueryException();
        }

        
    }
}

public record UpdateStaffInput (int StaffId, string StaffName, int RoleId);