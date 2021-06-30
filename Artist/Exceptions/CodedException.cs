using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Artist.Exceptions
{
    public abstract class CodedException : Exception
    {
        public abstract int Code { get; }

        public CodedException()
        {
        }

        public CodedException(string message)
            : base(message)
        {
        }

        public CodedException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        public CodedException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
