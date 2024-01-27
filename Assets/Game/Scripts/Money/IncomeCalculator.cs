using Game.Scripts.StarSystem.Common;

namespace Game.Scripts.Money
{
    public static class IncomeCalculator
    {
        public static float PerAxisIncome(float axisPerSecond, int depth)
        {
            return axisPerSecond * StaticData.IncomeByDepth.Evaluate(depth);
        }

        public static float PerOrbitIncome(float orbitPerSecond, int depth, float orbitRadius)
        {
            return orbitPerSecond * StaticData.IncomeByDepth.Evaluate(depth) * orbitRadius;
        }

        public static float IncomePerClick => StaticData.IncomePerClickByPlanetsCount.Evaluate(SpaceBody.TotalBodiesCount);

    }
}