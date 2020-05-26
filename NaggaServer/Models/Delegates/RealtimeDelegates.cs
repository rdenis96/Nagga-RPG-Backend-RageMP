using Domain.Models.Players;
using GTANetworkAPI;

namespace NaggaServer.Models.Delegates
{
    public delegate void OnPlayerSignedIn(Player player, PlayerInfoWrapper dbPlayer);
    public delegate void OnPlayerSignedOut(Player player);
}
