using WebApplication1.Models;

namespace WebApplication1.GraphQL.Queries;

[ExtendObjectType(Name = "Query")]
public class StaffRoleQueries
{
    public IQueryable<StaffRole> GetStaffRole(ApplicationDbContext dbContext, int? staffRoleId)
    {
        if (staffRoleId != null)
        {
            return dbContext.StaffRoles.Where(x => x.RoleId == staffRoleId);
        }
        return dbContext.StaffRoles;
    }
}