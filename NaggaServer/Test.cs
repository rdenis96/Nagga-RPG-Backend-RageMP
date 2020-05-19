using GTANetworkAPI;

public class Test : Script
{
    public Test()
    {

    }

    [ServerEvent(Event.ResourceStart)]
    public void myResourceStart()
    {
        NAPI.Util.ConsoleOutput("Starting mines!");
    }

    [Command("mine")]
    public void PlaceMine(Player sender, float MineRange = 10f)
    {
        var pos = NAPI.Entity.GetEntityPosition(sender);
        var playerDimension = NAPI.Entity.GetEntityDimension(sender);

        var prop = NAPI.Object.CreateObject(NAPI.Util.GetHashKey("prop_bomb_01"), pos - new Vector3(0, 0, 1f), new Vector3(), 255, playerDimension);
        var shape = NAPI.ColShape.CreateSphereColShape(pos, 10);
        shape.Dimension = playerDimension;

        bool mineArmed = false;

        shape.OnEntityEnterColShape += (s, ent) =>
        {
            if (!mineArmed) return;

            NAPI.Explosion.CreateOwnedExplosion(sender, ExplosionType.ProxMine, pos, 1f, playerDimension);
            NAPI.Entity.DeleteEntity(prop);
            NAPI.ColShape.DeleteColShape(shape);
        };

        shape.OnEntityExitColShape += (s, ent) =>
        {
            if (ent == sender.Handle && !mineArmed)
            {
                mineArmed = true;
                NAPI.Notification.SendNotificationToPlayer(sender, "Mine has been ~r~armed~w~!", true);
            }
        };
    }
}