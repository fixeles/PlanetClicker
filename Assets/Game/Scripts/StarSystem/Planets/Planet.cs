using System;
using FPS.Pool;
using Game.Scripts.Money;
using Game.Scripts.Money.Upgrades;
using Game.Scripts.StarSystem.Common;
using Game.Scripts.StarSystem.Stars;
using Game.Scripts.UI;

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
                    StaticData.Income.PerAxisForPlanet(Depth, UpgradeData.IncomeUpgrade.Level) * motionData.AxisPerSecond +
                    StaticData.Income.PerOrbit(Depth, UpgradeData.IncomeUpgrade.Level) * motionData.OrbitPerSecond;
            }
        }
        public override double NextUpgradeIncomePerSecond
        {
            get
            {
                var motionData = MotionData as PlanetMotionData;
                return
                    StaticData.Income.PerAxisForPlanet(Depth, UpgradeData.IncomeUpgrade.Level + 1) * motionData.AxisPerSecond +
                    StaticData.Income.PerOrbit(Depth, UpgradeData.IncomeUpgrade.Level + 1) * motionData.OrbitPerSecond;
            }
        }

        public PlanetMotionData PlanetMotionData => MotionData as PlanetMotionData;

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
            double reward = StaticData.Income.PerAxisForPlanet(Depth, UpgradeData.IncomeUpgrade.Level);
            Wallet.AddMoney(reward);
            FluffyPool.Get<UIFloatingText>().ConfigureForTarget(View.CachedTransform, $"+{reward.ToShortString()}");
        }

        private void AddOrbitReward()
        {
            double reward = StaticData.Income.PerOrbit(Depth, UpgradeData.IncomeUpgrade.Level);
            Wallet.AddMoney(reward);
            FluffyPool.Get<UIFloatingText>().ConfigureForTarget(View.CachedTransform, $"+{reward.ToShortString()}");
        }
    }
}