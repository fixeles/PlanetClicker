namespace Game.Scripts.Money.Upgrades
{
    public class PlanetUpgradeData : SpaceBodyUpgradeData
    {
        public readonly Upgrade OrbitSpeedUpgrade;

        public PlanetUpgradeData()
        {
            OrbitSpeedUpgrade = new();
        }
    }
}