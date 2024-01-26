using Cinemachine;
using UnityEngine;

namespace Game.Scripts
{
    public class CameraController : MonoBehaviour
    {
        private static CameraController _instance;
        [SerializeField] private CinemachineVirtualCamera virtualCamera;

        private void Awake()
        {
            _instance = this;
        }

        public static void SwitchTarget(Transform target)
        {
            _instance.virtualCamera.Follow = target;
            _instance.virtualCamera.LookAt = target;
        }
    }
}