using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Artist.Exceptions;

namespace Artist.Exceptions
{
    public class DeleteNonExistMasterException : CodedException
    {
        public override int Code
        {
            get
            {
                return (int)Errors.DeleteNonExistMasterException;
            }
        }

        public DeleteNonExistMasterException(string message)
            : base(message)
        {
        }

        public DeleteNonExistMasterException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
