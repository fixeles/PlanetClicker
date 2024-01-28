using System;
using Game.Scripts.Money.Upgrades;
using Game.Scripts.StarSystem.Common;
using UnityEngine;

namespace Game.Scripts
{
    [CreateAssetMenu]
    public class StaticData : ScriptableObject
    {
        private static StaticData _instance;
        [SerializeField] private IncomeData _incomeData;
        [SerializeField] private PriceData _priceData;

        public static IncomeData Income => _instance._incomeData;
        public static PriceData Price => _instance._priceData;

        [Header("Planets data")]
        [SerializeField] private float satelliteSizeStep = 0.4f;



        public static float SatelliteSizeStep => _instance.satelliteSizeStep;

        public void Init() => _instance = this;



        [Serializable]
        public class IncomeData
        {
            [SerializeField] private AnimationCurve upgradeMultiplier;
            [SerializeField] private AnimationCurve perClickByTotalBodiesCount;
            [SerializeField] private AnimationCurve perAxisByDepth;
            [SerializeField] private AnimationCurve perOrbitByDepth;

            public float PerAxis(int depth, Upgrade incomeUpgrade)
            {
                return perAxisByDepth.Evaluate(depth) * upgradeMultiplier.Evaluate(incomeUpgrade.Level);
            }

            public float PerOrbit(int depth, Upgrade incomeUpgrade)
            {
                return perOrbitByDepth.Evaluate(depth) * upgradeMultiplier.Evaluate(incomeUpgrade.Level);
            }

            public double PerClick => perClickByTotalBodiesCount.Evaluate(SpaceBody.TotalBodiesCount);
        }


        [Serializable]
        public class PriceData
        {
            [SerializeField] private AnimationCurve multiplierByDepth;
            [SerializeField] private AnimationCurve multiplierBySatellitesCount;
            [SerializeField] private AnimationCurve priceByTotalBodiesCount;
            [SerializeField] private AnimationCurve upgradePriceByLevel;


            public double NextSatellitePrice(int depth, int satellitesCount)
            {
                return
                    priceByTotalBodiesCount.Evaluate(SpaceBody.TotalBodiesCount) *
                    multiplierByDepth.Evaluate(depth) *
                    multiplierBySatellitesCount.Evaluate(satellitesCount);
            }

            public double NextUpgradePrice(int depth, int level) => multiplierByDepth.Evaluate(depth) * upgradePriceByLevel.Evaluate(level + 1);
        }
    }
}