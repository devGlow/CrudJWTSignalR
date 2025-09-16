

using Projet101.Models;

namespace Projet101.DAL
{
    public interface IUnitOfWork
    {
        IGenericRepository<User> UserRepository { get; }
       
        IGenericRepository<T> GetRepository<T>() where T : class;
        void Dispose();
        void Save();
    }
}