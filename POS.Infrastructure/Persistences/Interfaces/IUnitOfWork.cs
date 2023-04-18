using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS.Infrastructure.Persistences.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        ICategoryRepository CategoryRepository { get; }

        void SaveChanges();
        Task SaveChangesAsync();
    }
}
