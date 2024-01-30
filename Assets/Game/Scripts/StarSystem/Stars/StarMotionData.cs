using System;
using Game.Scripts.Money.Upgrades;
using Game.Scripts.StarSystem.Common;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Game.Scripts.StarSystem.Stars
{
    [Serializable]
    public class StarMotionData : SpaceBodyMotionData
    {
        // [SerializeField] private float axisState = Random.Range(0, 0.5f);
        //[SerializeField] private float axisPerSecond = 0.03f;



        public StarMotionData(Upgrade axisUpgrade) : base(axisUpgrade)
        {
            AxisState = Random.Range(0, 0.5f);
            AxisPerSecond = 0.03f;
        }

        public override void Update(Vector3 parentPosition)
        {
            base.Update(parentPosition);
            Rotation = Quaternion.Euler(0, 0, 0) * Quaternion.Euler(0, -AxisState * 360, 0);
        }
    }
}