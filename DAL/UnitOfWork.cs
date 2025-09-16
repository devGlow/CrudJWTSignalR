using Projet101.Models;

namespace Projet101.DAL
{
    public class UnitOfWork(AppDbContext context) : IDisposable, IUnitOfWork
    {

        private readonly Dictionary<Type, object> _repositories = new Dictionary<Type, object>();
        public IGenericRepository<User> UserRepository
            => GetRepository<User>();
        
        // This is a generic lazy repository iniatialization 
        // Lazy loading = created only when needed
        public IGenericRepository<T> GetRepository<T>() where T : class
        {
            // The _repositories dictionary acts as a cache for repositories.
            // This helps optimize performance, reduce memory usage and maintain a single instance of each repo.
            if (_repositories.ContainsKey(typeof(T)))
            {
                return (IGenericRepository<T>)_repositories[typeof(T)];
            }
            var repository = new GenericRepository<T>(context);
            _repositories.Add(typeof(T), repository);

            return repository;

        }

        public void Save()
        {
            context.SaveChanges();

        }

        /* Like ay class that instantiates a database context in a class variable,
         * the UnitOfWork class implements IDisposable and disposes the context
         * which means release resources
         */

        // disposed checks if the UnitOfWork has already been disposed
       private bool disposed = false;
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
