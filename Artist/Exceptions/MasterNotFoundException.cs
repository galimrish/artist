using System;

namespace Artist.Exceptions
{
    class MasterNotFoundException : CodedException
    {
        public override int Code
        {
            get
            {
                return (int)Errors.MasterNotFoundException;
            }
        }

        public MasterNotFoundException(string message)
            : base(message)
        {
        }
    }
}
