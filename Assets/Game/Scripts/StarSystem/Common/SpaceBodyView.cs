using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Game.Scripts.StarSystem.Common
{
    public class SpaceBodyView : MonoBehaviour, IPointerClickHandler
    {
        public event Action SelectEvent;
        public int SkinId;

        [SerializeField] private TrailRenderer trail;
        [SerializeField] private Transform cachedTransform;

        public Transform CachedTransform => cachedTransform;

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