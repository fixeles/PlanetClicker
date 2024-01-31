using System;
using Random = UnityEngine.Random;

namespace Game.Scripts
{
    [Serializable]
    public struct Border
    {
        public float Min;
        public float Max;

        public float RandomValue => Random.Range(Min, Max);
    }
}