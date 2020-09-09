using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace IDCOLAdvanceModule.Model.CustomException
{
    public class UiException : Exception
    {
        public UiException()
            : base()
        {

        }

        public UiException(string message)
            : base(message)
        {

        }

        public UiException(string message, Exception innerException)
            : base(message, innerException)
        {

        }

        public UiException(SerializationInfo serializationInfo, StreamingContext context)
            : base(serializationInfo, context)
        {

        }
    }
}
