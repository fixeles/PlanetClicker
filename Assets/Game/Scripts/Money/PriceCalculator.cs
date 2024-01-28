using Game.Scripts.StarSystem.Common;

namespace Game.Scripts.Money
{
    public class PriceCalculator
    {
        public static double NextSatellitePrice(int depth, int satellitesCount)
        {
            return
                StaticData.PriceByTotalBodiesCount.Evaluate(SpaceBody.TotalBodiesCount) *
                StaticData.PriceByDepth.Evaluate(depth) *
                StaticData.PriceBySatellitesCount.Evaluate(satellitesCount);
        }
    }
}