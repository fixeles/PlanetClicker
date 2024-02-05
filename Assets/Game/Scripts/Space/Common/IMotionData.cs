using UnityEngine;

namespace Game.Scripts.Space.Common
{
    public interface IMotionData
    {
        Vector3 Position { get; }
        Quaternion Rotation { get; }
        void Update(Vector3 parentPosition);
    }
}