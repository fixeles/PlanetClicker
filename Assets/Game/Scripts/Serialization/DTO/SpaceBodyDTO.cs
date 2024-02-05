using Game.Scripts.Money.Upgrades;

namespace Game.Scripts.Serialization.DTO
{
    public class SpaceBodyDTO
    {
        public readonly PlanetDTO[] SatellitesDTO;
        public readonly SpaceBodyUpgradeData UpgradeData;
        public readonly int SkinId;

        protected SpaceBodyDTO(PlanetDTO[] satellitesDTO, SpaceBodyUpgradeData upgradeData, int skinId)
        {
            SatellitesDTO = satellitesDTO;
            UpgradeData = upgradeData;
            SkinId = skinId;
        }
    }
}