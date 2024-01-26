using System;
using Game.Scripts.StarSystem.Common;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Game.Scripts.StarSystem.Stars
{
    [Serializable]
    public class StarMotionData : IMotionData
    {
        [SerializeField] private float selfRotationState = Random.Range(-0.1f, 0.1f);
        [SerializeField] private float selfRotationPerSecond = 0.03f;
        
        public Vector3 Position => Vector3.zero;
        [field: SerializeField] public Quaternion Rotation { get; private set; }

        public void Update(Vector3 parentPosition)
        {
            selfRotationState += Time.deltaTime * selfRotationPerSecond;
            if (selfRotationState is > 1 or < 1)
            {
                selfRotationState %= 1;
                //add points
            }

            Rotation = Quaternion.Euler(0, 0, 0) * Quaternion.Euler(0, -selfRotationState * 360, 0);
        }
    }
}