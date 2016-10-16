using System;
using Salvis.Resources;

namespace  Salvis.App.Web.Models
{

    public class ValidationUIException : Exception
    {
        public MessageBox MessageBox { get; private set; }

        public ValidationUIException(string message)
            : base(message)
        {
            if (string.IsNullOrWhiteSpace(message)) throw new ArgumentNullException("message");
            MessageBox = new MessageBox { Message = message, Title = Texts.ErrorInValidation, Type = MessageBoxType.warning.ToString() };
        }
    }
}