using UnityEngine;

namespace Game.Scripts.UI
{
    public class ClickFX : MonoBehaviour
    {
        [SerializeField] private ParticleSystem fx;
        private Camera _camera;
        private Transform _fxTransform;

        private void Awake()
        {
            _camera = Camera.main;
            _fxTransform = fx.transform;
        }

        public void Play()
        {
            Vector3 mousePosition = Input.mousePosition;
            mousePosition.z = 5;
            var worldPoint = _camera.ScreenToWorldPoint(mousePosition);
            _fxTransform.position = worldPoint;
            fx.Play();
        }
    }
}