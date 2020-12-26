using BusinessLogic.Workers.Factions;
using BusinessLogic.Workers.Players;
using DataLayer.Factions;
using DataLayer.Repositories.Players;
using Domain.Repositories.Factions;
using Domain.Repositories.Players;
using Ninject;

namespace Heimdal.Backend.CompositionRoot
{
    public class CompositionRoot
    {
        private StandardKernel _kernel;
        private static volatile CompositionRoot _instance;
        private static object _syncRoot = new object();

        public static CompositionRoot Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_syncRoot)
                    {
                        if (_instance == null)
                        {
                            _instance = new CompositionRoot();
                        }
                    }
                }

                return _instance;
            }
        }

        public T GetImplementation<T>(string instanceName = null)
        {
            return _kernel.Get<T>(instanceName);
        }

        public T GetImplementation<T>()
        {
            return _kernel.Get<T>();
        }

        public CompositionRoot()
        {
            _kernel = new StandardKernel();

            _kernel.Bind<IPlayerRepository>().To<PlayerRepository>();
            _kernel.Bind<PlayersWorker>().To<PlayersWorker>()
                .WithConstructorArgument(GetImplementation<IPlayerRepository>());

            _kernel.Bind<IFactionInfoRepository>().To<FactionInfoRepository>();
            _kernel.Bind<FactionInfosWorker>().To<FactionInfosWorker>()
                .WithConstructorArgument(GetImplementation<IFactionInfoRepository>());
        }
    }
}