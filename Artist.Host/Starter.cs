using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.ServiceModel;
using Serilog;

namespace Artist.Host
{
    public class Starter
    {
        private CompositionRoot _compositionRoot;

        public void Start()
        {
            try
            {
                Log.Information("Service Artist start");

                var appSettings = Properties.Settings.Default.CreateAppSettings();
                _compositionRoot = new CompositionRoot(appSettings);

                Log.Information("Host start");
                var host = new ServiceHost(_compositionRoot.ArtistService);
                host.BaseAddresses.ToList().ForEach(a => Log.Information("BaseAddress:{0}", a));
                host.Open();

                Log.Information("Host is running");

                Log.Information("Automapper configuration");
                AutomapperConfig.Register();

                Log.Information("Service is running");
            }
            catch (Exception exc)
            {
                Log.Error(exc.Message);
                throw exc;
            }
        }

        public bool Stop()
        {
            Log.Information("Service has stopped");
            return true;
        }
    }
}
