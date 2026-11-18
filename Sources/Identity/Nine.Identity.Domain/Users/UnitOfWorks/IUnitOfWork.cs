namespace Nine.Identity.Domain.Users.UnitOfWorks;

public interface IUnitOfWork
{
    Task BeginTransactionAsync();

    Task CommitTransactionAsync();
}
