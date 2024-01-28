using System;
using Game.Scripts.Money;
using Game.Scripts.Money.Upgrades;
using Game.Scripts.StarSystem.Common;
using Game.Scripts.StarSystem.Stars;

namespace Game.Scripts.StarSystem.Planets
{
    public class Planet : SpaceBody
    {
        private readonly PlanetUpgradeData _upgradeData;
        private readonly float _orbitRadius;

        public override double IncomePerSecond
        {
            get
            {
                if (MotionData is PlanetMotionData motionData)
                    return (motionData.OrbitPerSecond + motionData.AxisPerSecond) * _upgradeData.IncomeUpgrade.Level;

                throw new Exception("Incorrect motion data");
            }
        }

        public Planet(SpaceBody parent)
        {
            float initialRadiusMultiplier;
            switch (parent)
            {
                case Star:
                    initialRadiusMultiplier = 10;
                    Size = parent.Size / 2 - StaticData.SatelliteSizeStep / 2 * (parent.SatellitesCount + 1);
                    break;
                case Planet:
                    initialRadiusMultiplier = 6;
                    Size = parent.Size - StaticData.SatelliteSizeStep * (parent.SatellitesCount + 1);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            if (parent.SatellitesCount > 0)
            {
                var lastSatellite = parent.Satellites[^1];
                _orbitRadius = lastSatellite._orbitRadius + lastSatellite.Size;
            }
            else
                _orbitRadius = initialRadiusMultiplier * parent.Size;


            Depth = parent.Depth + 1;
            var planetMotionData = new PlanetMotionData(_orbitRadius, Depth);
            MotionData = planetMotionData;

            Parent = parent;
            View = PlanetViewBuilder.Create(Size);
            Init();

            _upgradeData = new();
            planetMotionData.AxisTurnEvent += AddAxisReward;
            planetMotionData.OrbitTurnEvent += AddOrbitReward;
        }

        private void AddAxisReward()
        {
            Wallet.AddMoney(IncomeCalculator.PerAxisIncome(Depth, _upgradeData.IncomeUpgrade));
        }

        private void AddOrbitReward()
        {
            Wallet.AddMoney(IncomeCalculator.PerOrbitIncome(Depth, _orbitRadius, _upgradeData.IncomeUpgrade));
        }
    }
}