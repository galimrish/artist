using System;

namespace Artist.Exceptions
{
    class MasterExistException : CodedException
    {
        public override int Code
        {
            get
            {
                return (int)Errors.MasterExistException;
            }
        }

        public MasterExistException(string message)
            : base(message)
        {
        }
    }
}
