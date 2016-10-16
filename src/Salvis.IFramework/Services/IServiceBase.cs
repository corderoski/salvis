using System;
using System.Collections.Generic;

namespace Salvis.Framework.Services
{
    public interface IService : IDisposable
    {

    }

    public interface IServiceBase<T> : IService where T : class
    {
        T Get(int id);
        T Get(long id);
        IEnumerable<T> Get();
    }

    public interface IServiceBaseOperation<T> : IServiceBase<T> where T : class
    {
        T Add(T item);
        void Delete(T item);
        void Update(T item);
    }

    public interface IServiceBaseEnumerableOperation<T> : IServiceBase<T> where T : class
    {
        IEnumerable<T> Add(IEnumerable<T> items);
        void Delete(IEnumerable<T> items);
        void Update(IEnumerable<T> items);
    }
}
