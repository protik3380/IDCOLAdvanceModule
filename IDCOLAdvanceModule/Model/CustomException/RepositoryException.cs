using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace IDCOLAdvanceModule.Model.CustomException
{
    public class RepositoryException : Exception
    {
        public RepositoryException()
            : base()
        {

        }

        public RepositoryException(string message)
            : base(message)
        {

        }

        public RepositoryException(string message, Exception innerException)
            : base(message, innerException)
        {

        }

        public RepositoryException(SerializationInfo serializationInfo, StreamingContext context)
            : base(serializationInfo, context)
        {

        }
    }
}
