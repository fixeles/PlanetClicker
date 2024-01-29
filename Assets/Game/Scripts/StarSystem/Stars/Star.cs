using Game.Scripts.Money;
using Game.Scripts.StarSystem.Common;

namespace Game.Scripts.StarSystem.Stars
{
    public class Star : SpaceBody
    {
        public override double IncomePerSecond
        {
            get
            {
                return StaticData.Income.PerAxis(Depth, UpgradeData.IncomeUpgrade);
            }
        }

        public Star()
        {
            Size = 5;
            var starMotionData = new StarMotionData();
            MotionData = starMotionData;
            Parent = this;
            View = PlanetViewBuilder.Create(Size, PlanetViewBuilder.SpaceBodyType.Star);
            Init();

            UpgradeData = new();
            PlanetSelector.SelectedBody = this;
            starMotionData.AxisTurnEvent += AddAxisReward;
        }

        private void AddAxisReward()
        {
            Wallet.AddMoney(StaticData.Income.PerAxis(Depth, UpgradeData.IncomeUpgrade));
        }
    }
}