using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Artist.Interfaces
{
    public interface IAppSettings
    {
        int ConnectionTimeout { get; }
        TimeSpan DefaultCacheLifeTime { get; }
        string BonusUrlCCS { get; }
        string BonusUrlCIS { get; }
        bool GoldCrownConnection { get; }
        BonusProgram[] BonusPrograms { get; }
        X509Certificate BonusNet1Cert { get; }
    }
}
