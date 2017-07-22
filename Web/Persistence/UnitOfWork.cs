using NHibernate;
using NHibernate.Cfg;
using NHibernate.Tool.hbm2ddl;
using PersonalBlog.Web.Core;

namespace PersonalBlog.Web.Persistence
{
    /// <summary>
    /// 
    /// </summary>
    public class UnitOfWork : IUnitOfWork
    {
        /// <summary>
        /// 
        /// </summary>
        private static readonly ISessionFactory _sessionFactory = null;
        /// <summary>
        /// 
        /// </summary>
        private ITransaction _transaction;

        /// <summary>
        /// 
        /// </summary>
        public ISession Session { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        static UnitOfWork()
        {
            if (_sessionFactory != null) return;

            var cfg = new Configuration();
            cfg.Configure();
            cfg.AddAssembly("Web");

            new SchemaExport(cfg).Execute(false, true, false);

            _sessionFactory = cfg.BuildSessionFactory();
        }

        /// <summary>
        /// 
        /// </summary>
        public UnitOfWork()
        {
            Session = _sessionFactory.OpenSession();
        }

        /// <summary>
        /// 
        /// </summary>
        public void BeginTransaction()
        {
            _transaction = Session.BeginTransaction();
        }

        /// <summary>
        /// 
        /// </summary>
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

        /// <summary>
        /// 
        /// </summary>
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