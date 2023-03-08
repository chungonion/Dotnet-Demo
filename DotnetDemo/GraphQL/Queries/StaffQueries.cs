using WebApplication1.Models;

namespace WebApplication1.GraphQL.Queries;

[ExtendObjectType(Name = "Query")]
public class StaffQueries
{
    public IQueryable<Staff> GetStaff(ApplicationDbContext dbContext, int? staffId)
    {
        if (staffId != null)
        {
            return dbContext.Staffs.Where(x => x.StaffId == staffId);
        }
        return dbContext.Staffs;
    }
}