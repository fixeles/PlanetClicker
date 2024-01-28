namespace Game.Scripts.Money.Upgrades
{
    public class Upgrade
    {
        public int Level { get; private set; }

        public Upgrade(int level)
        {
            Level = level;
        }

        public Upgrade()
        {
            Level = 1;
        }

        public void AddLevel() => Level++;

        public double NextLevelPrice(int depth) => StaticData.PriceByDepth.Evaluate(depth) * StaticData.PriceByUpgradeLevel.Evaluate(Level + 1);
    }
}