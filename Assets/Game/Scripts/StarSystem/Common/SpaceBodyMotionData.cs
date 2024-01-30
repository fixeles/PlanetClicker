using System;
using Game.Scripts.Money.Upgrades;
using UnityEngine;

namespace Game.Scripts.StarSystem.Common
{
    public abstract class SpaceBodyMotionData : IMotionData
    {
        public event Action AxisTurnEvent;
        protected float AxisState;
        protected float AxisTilt;
        
        private readonly Upgrade _axisUpgrade;
        private float _axisPerSecond;
        
        public float NextUpgradeAxisPerSecond => _axisPerSecond + StaticData.Upgrade.AdditionalAxisSpeed(_axisUpgrade.Level + 1);
        
        public float AxisPerSecond
        {
            get => _axisPerSecond + StaticData.Upgrade.AdditionalAxisSpeed(_axisUpgrade.Level);
            protected set => _axisPerSecond = value;
        }

        public Vector3 Position { get; protected set; }
        public Quaternion Rotation { get; protected set; }


        protected SpaceBodyMotionData(Upgrade axisUpgrade)
        {
            _axisUpgrade = axisUpgrade;
        }


        public virtual void Update(Vector3 parentPosition)
        {
            AxisState += Time.deltaTime * AxisPerSecond;
            if (AxisState is > 1 or < -1)
            {
                AxisState %= 1;
                AxisTurnEvent?.Invoke();
            }
        }
    }
}