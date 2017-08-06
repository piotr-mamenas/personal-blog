using System;
using System.Collections.Generic;

namespace PersonalBlog.Web.ViewModels
{
    public class ViewModelCollection
    {
        private readonly Dictionary<Type, object> _models = new Dictionary<Type, object>();

        public void AddModel<T>(T t)
        {
            _models.Add(t.GetType(), t);
        }

        public T GetModel<T>()
        {
            return (T) _models[typeof(T)];
        }
    }
}