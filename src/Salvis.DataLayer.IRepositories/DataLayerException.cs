using System;

namespace Salvis.DataLayer.Repositories
{
    [Serializable]
    public class DataLayerException : Exception, IDataLayerException
    {

        private readonly Exception _exception;

        private readonly object _obj;

        public DataLayerException(string message, object obj = null)
            : base(message)
        {
            _exception = new Exception(message);
            _obj = obj;
        }

        public DataLayerException(Exception exception, object obj = null)
        {
            _exception = exception;
            _obj = obj;
        }

        public new string Message
        {
            get
            {
                return _exception.Message;
            }
        }

        public string InnerMessage
        {
            get
            {
                if (_exception.InnerException.InnerException != null)
                    return _exception.InnerException.InnerException.Message;
                return  _exception.InnerException.Message;
            }
        }

        public object RelatedObject
        {
            get
            {
                return _obj;
            }
        }

    }

}
