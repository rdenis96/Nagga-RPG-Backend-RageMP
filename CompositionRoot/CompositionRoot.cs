using BusinessLogic.Workers.Players;
using DataLayer.Repositories.Players;
using Domain.Repositories.Players;
using Ninject;

namespace Heimdal.Backend.CompositionRoot
{
    public class CompositionRoot
    {
        private StandardKernel kernel;
        private static volatile CompositionRoot instance;
        private static object syncRoot = new object();

        public static CompositionRoot Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (syncRoot)
                    {
                        if (instance == null)
                        {
                            instance = new CompositionRoot();
                        }
                    }
                }

                return instance;
            }
        }

        public T GetImplementation<T>(string instanceName = null)
        {
            return kernel.Get<T>(instanceName);
        }

        public T GetImplementation<T>()
        {
            return kernel.Get<T>();
        }

        public CompositionRoot()
        {
            kernel = new StandardKernel();

            kernel.Bind<IPlayerRepository>().To<PlayerRepository>();
            kernel.Bind<PlayersWorker>().To<PlayersWorker>()
                .WithConstructorArgument(GetImplementation<IPlayerRepository>());
        }
    }
}
