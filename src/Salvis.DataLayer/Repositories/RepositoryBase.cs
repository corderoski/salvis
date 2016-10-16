using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Data.Entity;
using Dapper;
using Salvis.DataLayer.Model;
using Salvis.DataLayer.Repositories.Factory;

namespace Salvis.DataLayer.Repositories
{

    internal class Schemas
    {
        public const String DEFAULT = "dbo";
    }

    /// <summary>
    /// Papa-upa
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class RepositoryBase<T> :
        IRepositoryBaseOperation<T>, IRepositoryBaseEnumerableOperation<T> where T : class
    {

        internal DataModelContainer Context;

        protected internal IDataLayerExceptionFactory DataLayerExceptionFactory;

        protected internal IDbConnection Connection;

        #region Constructor

        protected RepositoryBase(IDbConnection connection)
        {
            if (connection == null)
            {
                throw new ArgumentNullException("connection");
            }

            Context = new DataModelContainer("name=DbConnection ");

            InitComponents();
        }



        public bool TryConnection()
        {
            var result = Connection.Query("Select 1+1");
            return result != null && result.Any();
        }

        ~RepositoryBase()
        {
            Dispose(false);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected void Dispose(bool disposing)
        {
            if (disposing)
            {
                Connection = null;
                Context = null;
            }
        }

        #endregion

        #region IRepositoryBase Base Members

        public virtual T Get(int id)
        {
            return Get((long)id);
        }

        public virtual T Get(long id)
        {
            var sql = String.Format("SELECT * FROM {0} WHERE Id = @Id", EntityTableSchema, Schemas.DEFAULT);
            var query = Connection.Query<T>(sql, new { Id = id });
            return query.SingleOrDefault();
        }

        public IEnumerable<T> Get()
        {
            return Connection.GetList<T>();
        }

        protected internal IEnumerable<T> Get(object parameters)
        {
            return Connection.GetList<T>(parameters);
        }

        #endregion

        #region CRUD Single

        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
        /// <returns>A saved entity, otherwise, same passed object.</returns>
        public T Add(T item)
        {
            try
            {
                Context.Set<T>().Attach(item);
                Context.Entry(item).State = EntityState.Added;
                Context.SecureSaveChanges();
            }
            catch (Exception ex)
            {
                var dataException = DataLayerExceptionFactory.Create(ex, item);
                throw dataException;
            }

            return item;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
        public void Update(T item)
        {
            try
            {
                Context.Set<T>().Attach(item);
                Context.Entry(item).State = EntityState.Modified;
                Context.SecureSaveChanges();
            }
            catch (Exception ex)
            {
                var dataException = DataLayerExceptionFactory.Create(ex, item);
                throw dataException;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ids"></param>
        /// <param name="field"></param>
        public void Delete(long id, String field = "Id")
        {
            var sql = String.Format("DELETE FROM {0} WHERE {1} = @Id", EntityTableSchema, field);
            var query = Connection.Execute(sql, new { Id = id });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
        public void Delete(T item)
        {
            try
            {
                Context.Set<T>().Remove(item);
                Context.Entry(item).State = EntityState.Deleted;
                Context.SecureSaveChanges();
            }
            catch (Exception ex)
            {
                var dataException = DataLayerExceptionFactory.Create(ex, item);
                throw dataException;
            }
        }

        #endregion

        #region CRUD Collection

        /// <summary>
        /// 
        /// </summary>
        /// <param name="items"></param>
        /// <returns>A saved entity, otherwise, same passed object.</returns>
        public IEnumerable<T> Add(IEnumerable<T> items)
        {
            try
            {
                foreach (var item in items)
                {
                    Context.Set<T>().Attach(item);
                    Context.Entry(item).State = EntityState.Added;
                }
                Context.SecureSaveChanges();
            }
            catch (Exception ex)
            {
                var dataException = DataLayerExceptionFactory.Create(ex, items);
                throw dataException;
            }

            return items;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="items"></param>
        public void Update(IEnumerable<T> items)
        {
            try
            {
                foreach (var item in items)
                {
                    Context.Set<T>().Attach(item);
                    Context.Entry(item).State = EntityState.Modified;
                }
                Context.SecureSaveChanges();
            }
            catch (Exception ex)
            {
                var dataException = DataLayerExceptionFactory.Create(ex, items);
                throw dataException;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ids"></param>
        /// <param name="field"></param>
        public void Delete(IEnumerable<long> ids, String field = "Id")
        {
            var sql = String.Format("DELETE FROM {0} WHERE {1} = @Id", EntityTableSchema, field);
            foreach (var item in ids)
                Connection.Execute(sql, new { Id = item });
            
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="items"></param>
        public void Delete(IEnumerable<T> items)
        {
            try
            {
                foreach (var item in items)
                {
                    Context.Set<T>().Remove(item);
                    Context.Entry(item).State = EntityState.Deleted;
                }

                Context.SecureSaveChanges();
            }
            catch (Exception ex)
            {
                var dataException = DataLayerExceptionFactory.Create(ex, items);
                throw dataException;
            }
        }


        #endregion

        #region Internal & Private Functions

        protected internal String EntityTableSchema
        {
            get
            {
                return !String.IsNullOrEmpty(Schemas.DEFAULT) ?
                        String.Format("{1}.[{0}]", typeof(T).Name, Schemas.DEFAULT) :
                        String.Format("[{0}]", typeof(T).Name);
            }
        }

        /// <summary>
        /// Looks for a new Code checking if exists given a sql query.
        /// </summary>
        /// <param name="sql">a String containing the Query and a 'code' paraneter for replace.</param>
        /// <returns>a String containing the new Code.</returns>
        protected internal String GetNewCode(String sql)
        {
            var code = Guid.NewGuid();

            while (Connection.Query(sql, new { code }).Any())
                code = Guid.NewGuid();

            return code.ToString();
        }

        /// <summary>
        /// Initialize and sets initial configurations for the container.
        /// </summary>
        private void InitComponents()
        {
            Connection = Context.GetInstance();
            Context.Configuration.LazyLoadingEnabled = false;
            Context.Configuration.ProxyCreationEnabled = false;
            //
            DataLayerExceptionFactory = new DataLayerExceptionFactory();
        }

        #endregion

    }
}
