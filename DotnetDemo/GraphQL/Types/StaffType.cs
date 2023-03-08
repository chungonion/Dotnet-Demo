using Microsoft.EntityFrameworkCore;
using WebApplication1.GraphQL.DataLoaders;
using WebApplication1.Models;

namespace WebApplication1.GraphQL.Types;

public class StaffType : ObjectType<Staff>
{
    protected override void Configure(IObjectTypeDescriptor<Staff> descriptor)
    {
        descriptor
            .ImplementsNode()
            .IdField(x => x.StaffId)
            .ResolveNode((ctx, id) => ctx.DataLoader<StaffDataLoader>().LoadAsync(id, ctx.RequestAborted));

        descriptor.Field(x => x.StaffRole)
            .ResolveWith<StaffResolver>(x => x.GetStaffRoles(default!, default!, default!, default))
            .UseDbContext<ApplicationDbContext>();
    }

    private class StaffResolver
    {
        public async Task<IEnumerable<StaffRole>> GetStaffRoles(
            [Parent] Staff staff,
            ApplicationDbContext dbContext,
            StaffRoleDataLoader dataLoader,
            CancellationToken cancellationToken
        )
        {
            int[] staffIds = await dbContext.Staffs.Where(x => x.StaffId == staff.StaffId).Select(x => x.StaffId)
                .ToArrayAsync();
            return await dataLoader.LoadAsync(staffIds, cancellationToken);
        }
    }
}