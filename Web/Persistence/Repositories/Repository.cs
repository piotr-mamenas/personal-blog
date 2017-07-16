using System.Collections.Generic;
using NHibernate;
using NHibernate.Linq;
using PersonalBlog.Web.Core;
using PersonalBlog.Web.Core.Repositories;

namespace PersonalBlog.Web.Persistence.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly UnitOfWork _unitOfWork;

        public Repository(IUnitOfWork unitOfWork)
        {
            _unitOfWork = (UnitOfWork)unitOfWork;
        }

        protected ISession Session => _unitOfWork.Session;

        #region IRepository Members

        public IEnumerable<T> GetAll()
        {
            return Session.Query<T>();
            
        }

        public T GetById(int id)
        {
            return Session.Get<T>(id);
        }

        public void Create(T entity)
        {
            using (var transaction = Session.BeginTransaction())
            {
                Session.Save(entity);
                transaction.Commit();
            }
        }

        public void Update(T entity)
        {
            using (var transaction = Session.BeginTransaction())
            {
                Session.Update(entity);
                transaction.Commit();
            }
        }

        public void Delete(int id)
        {
            using (var transaction = Session.BeginTransaction())
            {
                Session.Delete(Session.Load<T>(id));
                transaction.Commit();
            }
        }

        #endregion
    }
}