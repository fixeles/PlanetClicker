using System;
using Cinemachine;
using UnityEngine;

namespace Game.Scripts.Core
{
    public class CameraController : MonoBehaviour
    {
        private static CameraController _instance;
        [SerializeField] private CinemachineVirtualCamera virtualCamera;

        private void Awake()
        {
            _instance = this;
            PlanetSelector.SelectedObjectChangedEvent += FollowSelectedObject;
        }

        private void Start()
        {
            FollowSelectedObject();
        }

        private static void FollowSelectedObject()
        {
            SwitchTarget(PlanetSelector.SelectedBody.View.transform);
        }

        private static void SwitchTarget(Transform target)
        {
            _instance.virtualCamera.Follow = target;
            _instance.virtualCamera.LookAt = target;
        }
    }
}