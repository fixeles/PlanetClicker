using System;
using Game.Scripts.StarSystem.Planets;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Game.Scripts.StarSystem.Common
{
    public class SpaceBodyView : MonoBehaviour, IPointerClickHandler
    {
        public event Action SelectEvent;
#if UNITY_EDITOR
        [SerializeField] private PlanetMotionData motionData;
#endif

        [SerializeField] private TrailRenderer trail;
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

        public void Configure(float planetSize)
        {
            trail.startWidth *= planetSize;
            cachedTransform.localScale = Vector3.one * planetSize;
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            SelectEvent?.Invoke();
        }

        private void OnValidate()
        {
            cachedTransform ??= transform;
        }
    }
}