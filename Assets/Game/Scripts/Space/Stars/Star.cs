using System.Linq;
using FPS.Pool;
using Game.Scripts.Core;
using Game.Scripts.Money;
using Game.Scripts.Serialization.DTO;
using Game.Scripts.Space.Common;
using Game.Scripts.UI;

namespace Game.Scripts.Space.Stars
{
    public class Star : SpaceBody, IDTOBuilder<StarDTO>
    {
        public override double IncomePerSecond
        {
            get
            {
                return StaticData.Income.PerAxisForStar(Depth, UpgradeData.IncomeUpgrade.Level) * StarMotionData.AxisPerSecondProp;
            }
        }
        public override double NextUpgradeIncomePerSecond
        {
            get
            {
                return StaticData.Income.PerAxisForStar(Depth, UpgradeData.IncomeUpgrade.Level + 1) * StarMotionData.AxisPerSecondProp;
            }
        }

        public StarMotionData StarMotionData => MotionData as StarMotionData;

        private void AddAxisReward()
        {
            double reward = StaticData.Income.PerAxisForPlanet(Depth, UpgradeData.IncomeUpgrade.Level);
            Wallet.AddMoney(reward);
            FluffyPool.Get<UIFloatingText>().ConfigureForTarget(View.CachedTransform, $"+{reward.ToShortString()}");
        }

        public StarDTO DTO => new(MotionData.DTO, Satellites.Select(satellite => satellite.DTO).ToArray(), UpgradeData, View.SkinId);

        public Star(StarDTO dto = null)
        {
            if (dto == null)
                InitAsNew();
            else
                InitByDTO(dto);
        }

        public void InitAsNew()
        {
            Size = 5;
            UpgradeData = new();
            var starMotionData = new StarMotionData(UpgradeData.AxisSpeedUpgrade);
            MotionData = starMotionData;
            Parent = this;
            View = PlanetViewBuilder.Create(Size, bodyType: PlanetViewBuilder.SpaceBodyType.Star);
            Init();

            starMotionData.AxisTurnEvent += AddAxisReward;
        }

        public void InitByDTO(StarDTO dto)
        {
            Size = 5;
            UpgradeData = dto.UpgradeData;
            var starMotionData = new StarMotionData(UpgradeData.AxisSpeedUpgrade, dto.MotionDTO);
            MotionData = starMotionData;
            Parent = this;
            View = PlanetViewBuilder.Create(Size, dto.SkinId, PlanetViewBuilder.SpaceBodyType.Star);
            Init();

            foreach (var planetDTO in dto.SatellitesDTO)
            {
                CreateSatellite(planetDTO);
            }

            starMotionData.AxisTurnEvent += AddAxisReward;
        }
    }
}