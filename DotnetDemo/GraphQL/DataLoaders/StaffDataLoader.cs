using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;

namespace WebApplication1.GraphQL.DataLoaders;

public class StaffDataLoader : BatchDataLoader<int, Staff>
{
    private readonly IDbContextFactory<ApplicationDbContext> _dbContextFactory;

    public StaffDataLoader(IBatchScheduler batchScheduler, DataLoaderOptions? options,
        IDbContextFactory<ApplicationDbContext> dbContextFactory) : base(batchScheduler, options)
    {
        _dbContextFactory = dbContextFactory ?? throw new ArgumentNullException(nameof(dbContextFactory));
    }

    protected override async Task<IReadOnlyDictionary<int, Staff>> LoadBatchAsync(IReadOnlyList<int> keys,
        CancellationToken cancellationToken)
    {
        await using ApplicationDbContext context = await _dbContextFactory.CreateDbContextAsync(cancellationToken);
        return await context.Staffs
            .Where(x => keys.Contains(x.StaffId))
            .ToDictionaryAsync(x => x.StaffId, cancellationToken);
    }
}