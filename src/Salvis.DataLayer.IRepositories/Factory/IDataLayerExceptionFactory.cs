using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Salvis.DataLayer.Repositories.Factory
{
    public interface IDataLayerExceptionFactory
    {
        DataLayerException Create(Exception ex, object obj = null);
        DataLayerException Create(string message, object obj = null);
    }
}
