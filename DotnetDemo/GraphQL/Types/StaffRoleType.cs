using Microsoft.EntityFrameworkCore;
using WebApplication1.GraphQL.DataLoaders;
using WebApplication1.Models;

namespace WebApplication1.GraphQL.Types;

public class StaffRoleType : ObjectType<StaffRole>
{
    protected override void Configure(IObjectTypeDescriptor<StaffRole> descriptor)
    {
        descriptor
            .ImplementsNode()
            .IdField(x => x.RoleId)
            .ResolveNode((ctx, id) => ctx.DataLoader<StaffRoleDataLoader>().LoadAsync(id, ctx.RequestAborted));

        descriptor.Field(x => x.Staffs)
            .ResolveWith<StaffRoleResolver>(x => x.GetStaffs(default!, default!, default!, default))
            .UseDbContext<ApplicationDbContext>();
    }

    private class StaffRoleResolver
    {
        public async Task<IEnumerable<Staff>> GetStaffs(
            [Parent] StaffRole staffRole,
            ApplicationDbContext dbContext,
            StaffDataLoader dataLoader,
            CancellationToken cancellationToken
        )
        {
            int[] staffIds = await
                dbContext.Staffs
                    .Where(x => x.RoleId == staffRole.RoleId)
                    .Select(x => x.StaffId)
                    .ToArrayAsync(cancellationToken: cancellationToken);
            return await dataLoader.LoadAsync(staffIds, cancellationToken);
        }

        public string Troll()
        {
            return "troll";
        }
    }
}