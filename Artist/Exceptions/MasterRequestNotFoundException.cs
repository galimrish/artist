using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Artist.Exceptions;

namespace Artist.Exceptions
{
    public class MasterRequestNotFoundException : CodedException
    {
        public override int Code
        {
            get
            {
                return (int)Errors.MasterRequestNotFoundException;
            }
        }

        public MasterRequestNotFoundException(string message)
            : base(message)
        {
        }

        public MasterRequestNotFoundException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
