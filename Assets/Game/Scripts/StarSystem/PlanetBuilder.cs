using Game.Scripts.StarSystem.Common;
using UnityEngine;

namespace Game.Scripts.StarSystem
{
    public class PlanetBuilder : MonoBehaviour
    {
        private static PlanetBuilder _instance;

        [SerializeField] private SpaceBodyView viewContainerPrefab;
        [SerializeField] private MeshFilter[] planets;
        [SerializeField] private MeshFilter[] stars;

        private void Awake()
        {
            _instance = this;
        }

        public static SpaceBodyView Create(float size, SpaceBodyType bodyType = SpaceBodyType.Planet)
        {
            var newBody = Instantiate(_instance.viewContainerPrefab);
            newBody.Configure(size);

            switch (bodyType)
            {
                case SpaceBodyType.Planet:
                     Instantiate(_instance.planets.GetRandomElement(), newBody.transform);
                    break;

                case SpaceBodyType.Star:
                    var mesh = Instantiate(_instance.stars.GetRandomElement(), newBody.transform);
                    break;
            }

            return newBody;
        }

        public enum SpaceBodyType
        {
            Planet,
            Star
        }
    }
}