using System;
using Game.Scripts.Money;
using Game.Scripts.Money.Upgrades;
using Game.Scripts.StarSystem.Common;
using Game.Scripts.StarSystem.Stars;

namespace Game.Scripts.StarSystem.Planets
{
    public class Planet : SpaceBody
    {
        private readonly float _orbitRadius;

        public override double IncomePerSecond
        {
            get
            {
                var motionData = MotionData as PlanetMotionData;
                return
                    StaticData.Income.PerAxisForPlanet(Depth, UpgradeData.IncomeUpgrade) * motionData.AxisPerSecond +
                    StaticData.Income.PerOrbit(Depth, UpgradeData.IncomeUpgrade) * motionData.OrbitPerSecond;
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


            var planetUpgradeData = new PlanetUpgradeData();
            UpgradeData = planetUpgradeData;
            Depth = parent.Depth + 1;
            var planetMotionData = new PlanetMotionData(_orbitRadius, Depth, planetUpgradeData.AxisSpeedUpgrade, planetUpgradeData.OrbitSpeedUpgrade);
            MotionData = planetMotionData;

            Parent = parent;
            View = PlanetViewBuilder.Create(Size);
            Init();

            planetMotionData.AxisTurnEvent += AddAxisReward;
            planetMotionData.OrbitTurnEvent += AddOrbitReward;
        }

        private void AddAxisReward()
        {
            Wallet.AddMoney(StaticData.Income.PerAxisForPlanet(Depth, UpgradeData.IncomeUpgrade));
        }

        private void AddOrbitReward()
        {
            Wallet.AddMoney(StaticData.Income.PerOrbit(Depth, UpgradeData.IncomeUpgrade));
        }
    }
}