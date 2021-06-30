using System;
using System.Security.Cryptography.X509Certificates;
using Artist.Interfaces;

namespace Artist
{
    public class AppSettings : IAppSettings
    {
        public int ConnectionTimeout { get; set; }
        public TimeSpan DefaultCacheLifeTime { get; set; }
        public string BonusUrlCCS { get; set; }
        public string BonusUrlCIS { get; set; }
        public bool GoldCrownConnection { get; set; }
        public BonusProgram[] BonusPrograms { get; set; }
        public X509Certificate BonusNet1Cert { get; set; }
    }
}
