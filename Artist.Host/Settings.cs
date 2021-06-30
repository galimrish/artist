using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using Artist.Helpers;
using Serilog;

namespace Artist.Host.Properties
{
    internal sealed partial class Settings 
    {
        private FileSystemWatcher _fileSystemWatcher;
        private AppSettings _appSettings;

        public Settings()
        {
            var configFileName = Path.GetFileName(AppDomain.CurrentDomain.SetupInformation.ConfigurationFile);
            var configPath = Path.GetDirectoryName(AppDomain.CurrentDomain.SetupInformation.ConfigurationFile);

            _fileSystemWatcher = new FileSystemWatcher
            {
                Path = configPath,
                NotifyFilter = NotifyFilters.LastWrite,
                EnableRaisingEvents = true,
                Filter = configFileName
            };

            _fileSystemWatcher.Changed += AppConfigChanged;
        }

        public AppSettings CreateAppSettings()
        {
            _appSettings = new AppSettings();
            SetAppSettings();
            return _appSettings;
        }

        void AppConfigChanged(object sender, FileSystemEventArgs e)
        {
            Default.Reload();
            SetAppSettings();
            Log.Information("AppConfig Path:{Path} Filename:{configFileName}",
                                _fileSystemWatcher.Path, _fileSystemWatcher.Filter);
        }

        void SetAppSettings()
        {
            _appSettings.ConnectionTimeout = Default.ConnectionTimeout;
            _appSettings.DefaultCacheLifeTime = Default.DefaultCacheLifeTime;
            
            _appSettings.BonusUrlCCS = Default.Bonus_URL_CCS;
            _appSettings.BonusUrlCIS = Default.Bonus_URL_CIS;
            _appSettings.GoldCrownConnection = Default.GoldCrownConnection;

            if (_appSettings.GoldCrownConnection == true)
            {
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12 | SecurityProtocolType.Ssl3;
                ServicePointManager.Expect100Continue = true;

                _appSettings.BonusPrograms = Default.Bonus_Program_Info_Json_Array.AsArray();

                ValidateBonusPrograms(_appSettings.BonusPrograms);

                var bonusCerts = FindCertificates(
                    storeName: Default.Bonus_CertificateStoreName,
                    storeLocation: Default.Bonus_CertificateStoreLocation,
                    bonusPrograms: _appSettings.BonusPrograms);

                _appSettings.BonusNet1Cert = bonusCerts[_appSettings.BonusPrograms[0].TYPE7813];
            }
        }

        private void ValidateBonusPrograms(BonusProgram[] bonusPrograms)
        {
            throw new NotImplementedException();
        }

        private static Dictionary<string, X509Certificate2> FindCertificates(string storeName, string storeLocation, BonusProgram[] bonusPrograms)
        {
            if (!Enum.TryParse(storeLocation, true, out StoreLocation location))
            {
                throw new InvalidOperationException("Invalid location");
            }

            X509Store store = new X509Store(storeName, location);
            store.Open(OpenFlags.ReadOnly);

            var certList = new List<X509Certificate2>();

            foreach (X509Certificate2 cert in store.Certificates)
            {
                certList.Add(cert);
            }

            var certs = certList.ToLookup(c => c.Thumbprint.ToLower());

            var dict = new Dictionary<string, X509Certificate2>();

            foreach (var bp in bonusPrograms)
            {
                X509Certificate2 cert = certList.FirstOrDefault(c =>
                    c.Thumbprint.ToLower().Equals(bp.CertificateThumbprint.ToLower(), StringComparison.InvariantCultureIgnoreCase));
                dict[bp.TYPE7813] = cert ?? throw new InvalidOperationException($"Certificate not found for {bp}");
                Log.Information($"FriendlyName:{cert.FriendlyName}; bonus program:{bp}");
            }

            return dict;
        }
    }
}