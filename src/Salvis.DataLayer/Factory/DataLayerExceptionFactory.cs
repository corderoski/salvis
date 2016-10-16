using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Salvis.DataLayer.Repositories.Factory
{
    /// <summary>
    /// Class that implements the creation of DataLayerException. It's internal 'cause will just used by RepositoryBase.
    /// </summary>
    internal class DataLayerExceptionFactory : IDataLayerExceptionFactory
    {

        public DataLayerException Create(Exception ex, object obj = null)
        {
            return new DataLayerException(ex, obj);
        }

        public DataLayerException Create(String message, object obj = null)
        {
            return new DataLayerException(message, obj);
        }

    }
}
