using FPS.Pool;
using Game.Scripts.Money;
using Game.Scripts.StarSystem.Common;
using Game.Scripts.UI;

namespace Game.Scripts.StarSystem.Stars
{
    public class Star : SpaceBody
    {
        public override double IncomePerSecond
        {
            get
            {
                return StaticData.Income.PerAxisForStar(Depth, UpgradeData.IncomeUpgrade.Level) * StarMotionData.AxisPerSecond;
            }
        }
        public override double NextUpgradeIncomePerSecond
        {
            get
            {
                return StaticData.Income.PerAxisForStar(Depth, UpgradeData.IncomeUpgrade.Level + 1) * StarMotionData.AxisPerSecond;
            }
        }

        public StarMotionData StarMotionData => MotionData as StarMotionData;

        public Star()
        {
            Size = 5;
            UpgradeData = new();
            var starMotionData = new StarMotionData(UpgradeData.AxisSpeedUpgrade);
            MotionData = starMotionData;
            Parent = this;
            View = PlanetViewBuilder.Create(Size, PlanetViewBuilder.SpaceBodyType.Star);
            Init();

            PlanetSelector.SelectedBody = this;
            starMotionData.AxisTurnEvent += AddAxisReward;
        }

        private void AddAxisReward()
        {
            double reward = StaticData.Income.PerAxisForPlanet(Depth, UpgradeData.IncomeUpgrade.Level);
            Wallet.AddMoney(reward);
            FluffyPool.Get<UIFloatingText>().ConfigureForTarget(View.CachedTransform, $"+{reward.ToShortString()}");
        }
    }
}