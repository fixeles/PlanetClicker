using System;
using Game.Scripts.Money.Upgrades;
using Game.Scripts.StarSystem.Common;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Game.Scripts.StarSystem.Stars
{
    [Serializable]
    public class StarMotionData : IMotionData
    {
        public event Action AxisTurnEvent;
        [SerializeField] private float axisState = Random.Range(0, 0.5f);
        [SerializeField] private float axisPerSecond = 0.03f;
        [field: SerializeField] public Quaternion Rotation { get; private set; }

        private Upgrade _axisSpeedUpgrade;
        public float AxisPerSecond => axisPerSecond + StaticData.Upgrade.AdditionalAxisSpeed(_axisSpeedUpgrade.Level);

        public Vector3 Position => Vector3.zero;

        public StarMotionData(Upgrade axisSpeedUpgrade)
        {
            _axisSpeedUpgrade = axisSpeedUpgrade;
        }

        public void Update(Vector3 parentPosition)
        {
            axisState += Time.deltaTime * AxisPerSecond;
            if (axisState is > 1 or < 0)
            {
                axisState %= 1;
                AxisTurnEvent?.Invoke();
            }

            Rotation = Quaternion.Euler(0, 0, 0) * Quaternion.Euler(0, -axisState * 360, 0);
        }
    }
}