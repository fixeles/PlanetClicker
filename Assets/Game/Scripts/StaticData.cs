using UnityEngine;

namespace Game.Scripts
{
    [CreateAssetMenu]
    public class StaticData : ScriptableObject
    {
        private static StaticData _instance;

        [Header("Planets data")]
        [SerializeField] private float satelliteSizeStep = 0.4f;


        [Header("Income")]
        [SerializeField] private AnimationCurve incomeByDepth;
        [SerializeField] private AnimationCurve incomeByUpgrade;

        

        [Header("Price")]
        [SerializeField] private AnimationCurve priceByDepth;
        [SerializeField] private AnimationCurve priceByBodiesCount;
        [SerializeField] private AnimationCurve priceBySatellitesCount;
        [SerializeField] private AnimationCurve priceByUpgradeLevel;

        
        public static AnimationCurve IncomeByUpgrade => _instance.incomeByUpgrade;
        public static AnimationCurve PriceByUpgradeLevel => _instance.priceByUpgradeLevel;
        public static AnimationCurve PriceBySatellitesCount => _instance.priceBySatellitesCount;
        public static AnimationCurve PriceByTotalBodiesCount => _instance.priceByBodiesCount;
        public static AnimationCurve PriceByDepth => _instance.priceByDepth;
        public static AnimationCurve IncomeByDepth => _instance.incomeByDepth;
        public static float SatelliteSizeStep => _instance.satelliteSizeStep;

        public void Init() => _instance = this;
    }
}