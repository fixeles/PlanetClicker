using System;
using Game.Scripts.Money;
using Game.Scripts.Money.Upgrades;
using Game.Scripts.StarSystem.Common;

namespace Game.Scripts.StarSystem.Stars
{
    public class Star : SpaceBody
    {
        private readonly SpaceBodyUpgradeData _upgradeData;
        public override double IncomePerSecond
        {
            get
            {
                if (MotionData is StarMotionData motionData)
                    return motionData.AxisPerSecond * _upgradeData.IncomeUpgrade.Level;

                throw new Exception("Incorrect motion data");
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

            _upgradeData = new();
            PlanetSelector.SelectedBody = this;
            starMotionData.AxisTurnEvent += AddAxisReward;
        }

        private void AddAxisReward()
        {
            Wallet.AddMoney(IncomeCalculator.PerAxisIncome(Depth, _upgradeData.IncomeUpgrade));
        }
    }
}