using Microsoft.Extensions.Configuration;
using POS.Infrastructure.FileStorage;
using POS.Infrastructure.Persistences.Contexts;
using POS.Infrastructure.Persistences.Interfaces;

namespace POS.Infrastructure.Persistences.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly POSDBContext _context;
        public ICategoryRepository CategoryRepository { get; private set; }

        public IUserRepository UserRepository { get; private set; }

        public IAzureStorage AzureStorage { get; private set; }

        public UnitOfWork(POSDBContext context, IConfiguration configuration)
        {
            _context = context;
            CategoryRepository = new CategoryRepository(_context);
            UserRepository = new UserRepository(_context);
            AzureStorage = new AzureStorage(configuration);
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public async Task SaveChangesAsync()
        {
           await _context.SaveChangesAsync();
        }
    }
}
