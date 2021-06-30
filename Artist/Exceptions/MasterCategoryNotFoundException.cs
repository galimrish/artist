using System;

namespace Artist.Exceptions
{
    class MasterCategoryNotFoundException : CodedException
    {
        public override int Code
        {
            get
            {
                return (int)Errors.MasterNotFoundException;
            }
        }

        public MasterCategoryNotFoundException(string message)
            : base(message)
        {
        }
    }
}
