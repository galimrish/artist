using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Serilog;
using Topshelf;

namespace Artist.Host
{
    class Program
    {
        static void Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
                    .ReadFrom.AppSettings()
                    .CreateLogger();

            HostFactory.Run(x =>
            {

                x.Service<Starter>(s =>
                {
                    s.ConstructUsing(name => new Starter());
                    s.WhenStarted(starter => starter.Start());
                    s.WhenStopped(starter => starter.Stop());
                });

                x.RunAsLocalSystem();

                x.SetDescription("Mobile CRM Artist Service, 2018 (с)");
                x.SetDisplayName("Mobile CRM Artist Service");
                x.SetServiceName("MCRM.Artist");
            });
        }
    }
}
