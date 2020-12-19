using BusinessLogic.Workers.Players;
using GTANetworkAPI;
using Heimdal.Backend.CompositionRoot;
using System.Threading.Tasks;

namespace NaggaServer.Controllers
{
    public class ServerController : Script
    {
        private readonly CompositionRoot _compositionRoot;
        private readonly PlayersWorker _playersWorker;

        public ServerController()
        {
            _compositionRoot = CompositionRoot.Instance;
            _playersWorker = _compositionRoot.GetImplementation<PlayersWorker>();
        }

        #region ServerEvents

        [ServerEvent(Event.ResourceStart)]
        public async Task OnServerResourceStart()
        {
            NAPI.Server.SetAutoSpawnOnConnect(false);
            NAPI.Server.SetAutoRespawnAfterDeath(true);
            NAPI.Server.SetGlobalServerChat(false);
        }

        #endregion ServerEvents
    }
}