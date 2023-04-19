using POS.Infrastructure.FileStorage;

namespace POS.Infrastructure.Persistences.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        ICategoryRepository CategoryRepository { get; }
        IUserRepository UserRepository { get; }
        IAzureStorage AzureStorage { get; }

        void SaveChanges();
        Task SaveChangesAsync();
    }
}
