using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;

namespace WebApplication1.GraphQL.DataLoaders;

public class StaffRoleDataLoader : BatchDataLoader<int, StaffRole>
{
    private readonly IDbContextFactory<ApplicationDbContext> _dbContextFactory;

    public StaffRoleDataLoader(IBatchScheduler batchScheduler, DataLoaderOptions? options,
        IDbContextFactory<ApplicationDbContext> dbContextFactory) : base(batchScheduler, options)
    {
        _dbContextFactory = dbContextFactory ?? throw new ArgumentNullException(nameof(dbContextFactory));
    }

    protected override async Task<IReadOnlyDictionary<int, StaffRole>> LoadBatchAsync(IReadOnlyList<int> keys,
        CancellationToken cancellationToken)
    {
        await using ApplicationDbContext context = await _dbContextFactory.CreateDbContextAsync(cancellationToken);
        return await context.StaffRoles
            .Where(x => keys.Contains(x.RoleId))
            .ToDictionaryAsync(x => x.RoleId, cancellationToken);
    }
}