using System;
using System.Collections.Generic;
using System.Linq;


namespace Salvis.DataLayer.Repositories
{

    public interface IRepository<T> : IDisposable where T : class
    {
        Boolean TryConnection();
    }

    public interface IRepositoryBase<T> : IRepository<T> where T : class
    {
        T Get(int id);
        T Get(long id);
        IEnumerable<T> Get();
    }

    /// <summary>
    /// Interfaces that manages the base contracts for single-use Data types.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IRepositoryBaseOperation<T> : IRepositoryBase<T> where T : class
    {
        T Add(T item);
        void Update(T item);
        void Delete(long id, String field = "Id");
        void Delete(T item);
    }

    /// <summary>
    /// Interface that manages the base contracts for IEnumerables Data types.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IRepositoryBaseEnumerableOperation<T> : IRepositoryBase<T> where T : class
    {
        IEnumerable<T> Add(IEnumerable<T> items);
        void Update(IEnumerable<T> items);
        void Delete(IEnumerable<T> items);
        void Delete(IEnumerable<long> id, String field = "Id");
    }

}
