using Game.Scripts.StarSystem.Common;
using UnityEngine;

namespace Game.Scripts.StarSystem
{
    public class PlanetViewBuilder : MonoBehaviour
    {
        private static PlanetViewBuilder _instance;

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
            newBody.transform.localScale = Vector3.one * size;

            Component newMesh;
            switch (bodyType)
            {
                case SpaceBodyType.Star:
                    newMesh = Instantiate(_instance.stars.GetRandomElement(), newBody.transform);
                    break;

                default:
                    newMesh = Instantiate(_instance.planets.GetRandomElement(), newBody.transform);
                    break;
            }

            newMesh.transform.localRotation = new Quaternion(
                Random.Range(0f, 1f),
                Random.Range(0f, 1f),
                Random.Range(0f, 1f),
                Random.Range(0f, 1f)
            );
            return newBody;
        }

        public enum SpaceBodyType
        {
            Planet,
            Star
        }
    }
}