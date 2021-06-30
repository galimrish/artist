using System;

namespace Artist.Exceptions
{
    class ActionAlreadyExistsException : CodedException
    {
        public override int Code
        {
            get
            {
                return (int)Errors.MasterNotFoundException;
            }
        }

        public ActionAlreadyExistsException(string message)
            : base(message)
        {
        }
    }
}
