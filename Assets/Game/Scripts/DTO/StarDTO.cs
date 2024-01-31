using Game.Scripts.Money.Upgrades;

namespace Game.Scripts.DTO
{
    public class StarDTO : SpaceBodyDTO
    {
        public SpaceBodyMotionDTO MotionDTO;

        public StarDTO(SpaceBodyMotionDTO motionDTO, PlanetDTO[] satellitesDTO, SpaceBodyUpgradeData upgradeData, int skinId) : base(satellitesDTO, upgradeData, skinId)
        {
            MotionDTO = motionDTO;
        }
    }
}