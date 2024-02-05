using UnityEngine;

namespace Game.Scripts.Core
{
    public class AppInitializer : MonoBehaviour
    {
        [SerializeField] private StaticData staticData;

        private void Awake()
        {
            Application.targetFrameRate = 75;
            staticData.Init();
        }
    }
}