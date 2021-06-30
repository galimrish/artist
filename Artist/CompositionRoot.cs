using System.Threading;
using Autofac;
using Artist.Cache;
using Artist.Interfaces;
using Artist.Model;

namespace Artist
{
    public class CompositionRoot
    {
        private readonly IContainer _container;
        
        public IContainer Container
        {
            get { return _container; }
        }

        public IArtistService ArtistService
        {
            get { return _container.Resolve<IArtistService>(); }
        }

        public CompositionRoot(IAppSettings appSettings)
        {
            var builder = new ContainerBuilder();

            builder.Register(c => appSettings).SingleInstance();
            builder.RegisterType<Artist>().AsSelf().SingleInstance();
            builder.RegisterType<ArtistService>().As<IArtistService>().SingleInstance();
            builder.RegisterType<DbContextFactory>().As<IDbContextFactory>().SingleInstance();
            builder.RegisterType<MasterCategoryCache>().As<IMasterCategoryCache>().SingleInstance();
            builder.RegisterType<MasterRequestCache>().As<IMasterRequestCache>().SingleInstance();
            _container = builder.Build();
        }
    }
}
