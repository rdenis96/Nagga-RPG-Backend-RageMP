using BusinessLogic.Workers.Players;
using GTANetworkAPI;

namespace NaggaServer.Controllers
{
    public class ServerController : Script
    {
        private readonly PlayersWorker _playersWorker;
        public ServerController()
        {
            _playersWorker = new PlayersWorker();
        }

        [ServerEvent(Event.ResourceStart)]
        public void OnServerResourceStart()
        {
            NAPI.Server.SetAutoSpawnOnConnect(false);
            NAPI.Server.SetAutoRespawnAfterDeath(true);
        }

    }
}
