using Game.Scripts.Money.Upgrades;

namespace Game.Scripts.Serialization.DTO
{
    public class StarDTO : SpaceBodyDTO
    {
        public readonly SpaceBodyMotionDTO MotionDTO;

        public StarDTO(SpaceBodyMotionDTO motionDTO, PlanetDTO[] satellitesDTO, SpaceBodyUpgradeData upgradeData, int skinId) : base(satellitesDTO, upgradeData, skinId)
        {
            MotionDTO = motionDTO;
        }
    }
}