using System;
using Game.Scripts.DTO;
using Game.Scripts.Money.Upgrades;
using Game.Scripts.StarSystem.Common;
using UnityEngine;

namespace Game.Scripts.StarSystem.Stars
{
    [Serializable]
    public class StarMotionData : SpaceBodyMotionData
    {
        public StarMotionData(Upgrade axisUpgrade) : base(axisUpgrade)
        {
            InitAsNew();
        }

        public StarMotionData(Upgrade axisUpgrade, SpaceBodyMotionDTO dto) : base(axisUpgrade)
        {
            InitByDTO(dto);
        }

        public override void Update(Vector3 parentPosition)
        {
            base.Update(parentPosition);
            Rotation = Quaternion.Euler(0, 0, 0) * Quaternion.Euler(0, -AxisState * 360, 0);
        }
    }
}