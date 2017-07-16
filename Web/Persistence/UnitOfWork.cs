using NHibernate;
using NHibernate.Cfg;
using NHibernate.Tool.hbm2ddl;
using PersonalBlog.Web.Core;

namespace PersonalBlog.Web.Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        private static readonly ISessionFactory _sessionFactory = null;
        private ITransaction _transaction;

        public ISession Session { get; private set; }

        static UnitOfWork()
        {
            if (_sessionFactory != null) return;

            var cfg = new Configuration();
            cfg.Configure();
            cfg.AddAssembly("Web");

            new SchemaExport(cfg).Execute(false, true, false);

            _sessionFactory = cfg.BuildSessionFactory();
        }

        public UnitOfWork()
        {
            Session = _sessionFactory.OpenSession();
        }

        public void BeginTransaction()
        {
            _transaction = Session.BeginTransaction();
        }

        public void Commit()
        {
            try
            {
                if (_transaction != null && _transaction.IsActive)
                    _transaction.Commit();
            }
            catch
            {
                if (_transaction != null && _transaction.IsActive)
                    _transaction.Rollback();

                throw;
            }
            finally
            {
                Session.Dispose();
            }
        }

        public void Rollback()
        {
            try
            {
                if (_transaction != null && _transaction.IsActive)
                    _transaction.Rollback();
            }
            finally
            {
                Session.Dispose();
            }
        }
    }
}