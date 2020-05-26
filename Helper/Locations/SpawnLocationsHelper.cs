using Domain.Models.Common;

namespace Helper.Locations
{
    public static class SpawnLocationsHelper
    {
        public static readonly Vector3Wrapper CivilSpawnPosition;
        public static readonly Vector3Wrapper CivilSpawnRotation;
        static SpawnLocationsHelper()
        {
            CivilSpawnPosition = new Vector3Wrapper(-1036.755f, -2737.948f, 21.2772f);
            CivilSpawnRotation = new Vector3Wrapper(0, 0, 0);
        }
    }
}
