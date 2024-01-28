using System;
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

        public float AxisPerSecond => axisPerSecond;
        public Vector3 Position => Vector3.zero;

        public void Update(Vector3 parentPosition)
        {
            axisState += Time.deltaTime * axisPerSecond;
            if (axisState is > 1 or < 0)
            {
                axisState %= 1;
                AxisTurnEvent?.Invoke();
            }

            Rotation = Quaternion.Euler(0, 0, 0) * Quaternion.Euler(0, -axisState * 360, 0);
        }
    }
}