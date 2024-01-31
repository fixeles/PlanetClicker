using System;
using Game.Scripts.DTO;
using Game.Scripts.Money.Upgrades;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Game.Scripts.StarSystem.Common
{
    public abstract class SpaceBodyMotionData : IMotionData, IDTOBuilder<SpaceBodyMotionDTO>
    {
        public event Action AxisTurnEvent;
        protected float AxisState;
        protected float AxisTilt;
        protected float AxisPerSecond;

        public Upgrade AxisUpgrade { get; protected set; }

        public float NextUpgradeAxisPerSecond => AxisPerSecond + StaticData.Upgrade.AdditionalAxisSpeed(AxisUpgrade.Level + 1);

        public float AxisPerSecondProp
        {
            get => AxisPerSecond + StaticData.Upgrade.AdditionalAxisSpeed(AxisUpgrade.Level);
            protected set => AxisPerSecond = value;
        }

        public Vector3 Position { get; protected set; }
        public Quaternion Rotation { get; protected set; }


        protected SpaceBodyMotionData(Upgrade axisUpgrade)
        {
            AxisUpgrade = axisUpgrade;
        }


        public virtual void Update(Vector3 parentPosition)
        {
            AxisState += Time.deltaTime * AxisPerSecondProp;
            if (AxisState is > 1 or < -1)
            {
                AxisState %= 1;
                AxisTurnEvent?.Invoke();
            }
        }

        public SpaceBodyMotionDTO DTO
        {
            get => new(AxisState, AxisTilt, AxisPerSecond);
        }

        public virtual void InitAsNew()
        {
            AxisState = Random.Range(0, 0.5f);
            AxisPerSecondProp = 0.03f;
            AxisTilt = Random.Range(-60, 60);
        }

        public void InitByDTO(SpaceBodyMotionDTO dto)
        {
            AxisPerSecond = dto.AxisPerSecond;
            AxisState = dto.AxisState;
            AxisTilt = dto.AxisTilt;
        }
    }
}