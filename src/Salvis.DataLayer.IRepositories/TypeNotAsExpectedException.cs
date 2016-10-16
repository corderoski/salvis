using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Salvis.DataLayer.Repositories
{

    [Serializable]
    public class TypeNotAsExpectedException : Exception, IDataLayerException
    {

        private readonly object _obj;

        public TypeNotAsExpectedException(object obj = null)
            : base("The passed type or parameter is not matching")
        {
            _obj = obj;
        }

        public TypeNotAsExpectedException(string message, object obj = null)
            : base(message)
        {
            _obj = obj;
        }

        public object RelatedObject
        {
            get { return _obj; }
        }
    }
}
