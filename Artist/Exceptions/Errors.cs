using System;

namespace Artist.Exceptions
{
    public enum Errors
    {
        TechnicalError = -1000,
        DeleteNonExistMasterException = 100,
        MasterRequestNotFoundException = 200,
        MasterNotFoundException = 300,
        MasterExistException = 301,
        ActionAlreadyExists = 400,
        GoldCrownDeleteMasterException = 500,
        MasterCategoryNotFoundException = 600        
    }
}
