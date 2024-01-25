using UnityEngine;

namespace SolarSystem
{
    public abstract class AstronomicalObject : MonoBehaviour
    {
        [SerializeField] private float tilt;
        [SerializeField] private float selfRotationState;
        protected Transform CachedTransform { get; private set; }


        public Vector3 Position => CachedTransform.position;
        
        protected void Awake()
        {
            CachedTransform = transform;
        }

        private void Update()
        {
            CachedTransform.rotation = Quaternion.Euler(0, 0, -tilt) * Quaternion.Euler(0, -selfRotationState * 360, 0);
        }
    }
}