namespace Convoquei.Core.Genericos.UoW
{
    public interface IUnitOfWork
    {
        Task CommitAsync();
        Task RollbackAsync();
        Task BeginTransactionAsync();
        Task SaveChangesAsync();
    }
}
