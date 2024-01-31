using Game.Scripts.StarSystem.Common;
using UnityEngine;

namespace Game.Scripts.StarSystem
{
    public class PlanetViewBuilder : MonoBehaviour
    {
        private static PlanetViewBuilder _instance;

        [SerializeField] private SpaceBodyView viewContainerPrefab;
        [SerializeField] private MeshFilter[] planets;
        [SerializeField] private MeshFilter[] moons;
        [SerializeField] private MeshFilter[] stars;

        private void Awake()
        {
            _instance = this;
        }

        public static SpaceBodyView Create(float size, int skinId = -1, SpaceBodyType bodyType = SpaceBodyType.Planet)
        {
            var newBody = Instantiate(_instance.viewContainerPrefab);
            newBody.transform.localScale = Vector3.one * size;
            newBody.Configure(size);

            Component newMesh;
            switch (bodyType)
            {
                case SpaceBodyType.Star:
                    newMesh = Instantiate(skinId < 0 ? _instance.stars.GetRandomElement(out skinId) : _instance.stars[skinId], newBody.transform);
                    break;

                default:
                    if (size > 1.2f)
                    {
                        newMesh = Instantiate(skinId < 0 ? _instance.planets.GetRandomElement(out skinId) : _instance.planets[skinId], newBody.transform);
                    }
                    else
                    {
                        newMesh = Instantiate(skinId < 0 ? _instance.moons.GetRandomElement(out skinId) : _instance.moons[skinId], newBody.transform);
                        newMesh.transform.localScale = Vector3.one * 2;
                    }
                    break;
            }
            
            newBody.SkinId = skinId;
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