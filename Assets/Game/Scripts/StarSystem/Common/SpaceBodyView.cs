using System;
using Game.Scripts.StarSystem.Common;
using Game.Scripts.StarSystem.Planets;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Game.Scripts.StarSystem
{
    public class SpaceBodyView : MonoBehaviour, IPointerClickHandler
    {
        public event Action SpawnSatelliteRequest;
#if UNITY_EDITOR
        [SerializeField] private PlanetMotionData motionData;
#endif

        [SerializeField] private Transform cachedTransform;

        public void Init(IMotionData data)
        {
#if UNITY_EDITOR
            if (data is PlanetMotionData planetMotionData)
                motionData = planetMotionData;
#endif
        }

        public void UpdateTransform(IMotionData data)
        {
            cachedTransform.position = data.Position;
            cachedTransform.rotation = data.Rotation;
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            SpawnSatelliteRequest?.Invoke();
        }

        private void OnValidate()
        {
            cachedTransform ??= transform;
        }
    }

}