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
                var motionData = MotionData as StarMotionData;
                return StaticData.Income.PerAxisForStar(Depth, UpgradeData.IncomeUpgrade) * motionData.AxisPerSecond;
            }
        }

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
            double reward = StaticData.Income.PerAxisForPlanet(Depth, UpgradeData.IncomeUpgrade);
            Wallet.AddMoney(reward);
            FluffyPool.Get<UIFloatingText>().ConfigureForTarget(View.CachedTransform, $"+{reward.ToShortString()}");
        }
    }
}