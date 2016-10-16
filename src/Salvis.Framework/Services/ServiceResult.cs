using System;
using System.Collections.Generic;

namespace Salvis.Framework.Services
{
    public class ServiceResult : IServiceResult
    {
        readonly object _obj;
        readonly IEnumerable<String> _errors;

        internal ServiceResult(object obj, IEnumerable<String> msg)
        {
            _obj = obj;
            _errors = msg;
        }

        public object Result
        {
            get
            {
                return _obj;
            }
        }

        public IEnumerable<string> Errors
        {
            get
            {
                return _errors;
            }
        }

    }
}
