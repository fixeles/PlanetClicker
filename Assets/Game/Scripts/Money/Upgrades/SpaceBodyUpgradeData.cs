namespace Game.Scripts.Money.Upgrades
{
    public class SpaceBodyUpgradeData
    {
        public readonly Upgrade IncomeUpgrade;
        public readonly Upgrade AxisSpeedUpgrade;

        public SpaceBodyUpgradeData()
        {
            IncomeUpgrade = new();
            AxisSpeedUpgrade = new();
        }
    }
}