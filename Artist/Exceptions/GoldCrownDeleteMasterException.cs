using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Artist.Exceptions;

namespace Artist.Exceptions
{
    public class GoldCrownDeleteMasterException : CodedException
    {
        public override int Code
        {
            get
            {
                return (int)Errors.GoldCrownDeleteMasterException;
            }
        }

        public GoldCrownDeleteMasterException(string message)
            : base(message)
        {
        }

        public GoldCrownDeleteMasterException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
