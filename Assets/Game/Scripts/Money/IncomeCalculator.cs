using Game.Scripts.Money.Upgrades;
using Game.Scripts.StarSystem.Common;

namespace Game.Scripts.Money
{
    public static class IncomeCalculator
    {
        public static float PerAxisIncome(int depth, Upgrade incomeUpgrade)
        {
            return StaticData.IncomeByDepth.Evaluate(depth) * StaticData.IncomeByUpgrade.Evaluate(incomeUpgrade.Level);
        }

        public static float PerOrbitIncome(int depth, float orbitRadius, Upgrade incomeUpgrade)
        {
            return StaticData.IncomeByDepth.Evaluate(depth) * orbitRadius * StaticData.IncomeByUpgrade.Evaluate(incomeUpgrade.Level);
        }

        public static double IncomePerClick => SpaceBody.TotalIncomePerSecond;

    }
}