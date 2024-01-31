using Game.Scripts.Money.Upgrades;

namespace Game.Scripts.DTO
{
    public class PlanetDTO : SpaceBodyDTO
    {
        public readonly PlanetMotionDTO MotionDTO;

        public PlanetDTO(PlanetMotionDTO motionDTO, PlanetDTO[] satellitesDTO, PlanetUpgradeData upgradeData, int skinId) : base(satellitesDTO, upgradeData, skinId)
        {
            MotionDTO = motionDTO;
        }
    }
}