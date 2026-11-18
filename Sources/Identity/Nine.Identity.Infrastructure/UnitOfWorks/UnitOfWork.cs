using Nine.Identity.Domain.Users.UnitOfWorks;
using Nine.Identity.Infrastructure.DbContexts;

namespace Nine.Identity.Infrastructure.UnitOfWorks;

public sealed class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationIdentityDbContext _dbContext;

    public UnitOfWork(ApplicationIdentityDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task BeginTransactionAsync()
    {
        await _dbContext.Database.BeginTransactionAsync();
    }

    public async Task CommitTransactionAsync()
    {
        await _dbContext.Database.CommitTransactionAsync();
    }
}
