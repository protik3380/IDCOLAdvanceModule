using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace IDCOLAdvanceModule.Model.CustomException
{
    public class BllException : Exception
    {
        public BllException()
            : base()
        {

        }

        public BllException(string message)
            : base(message)
        {

        }

        public BllException(string message, Exception innerException)
            : base(message, innerException)
        {

        }

        public BllException(SerializationInfo serializationInfo, StreamingContext context)
            : base(serializationInfo, context)
        {

        }
    }
}
