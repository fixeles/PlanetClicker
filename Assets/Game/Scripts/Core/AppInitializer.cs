using UnityEngine;
using UnityEngine.SceneManagement;

namespace Game.Scripts.Core
{
    public class AppInitializer : MonoBehaviour
    {
        [SerializeField] private StaticData staticData;

        private void Awake()
        {
            Application.targetFrameRate = 75;
            staticData.Init();

#if UNITY_EDITOR || !UNITY_WEBGL
            StartGame();
#endif
        }

        public void StartGame()
        {
            SceneManager.LoadScene(sceneBuildIndex: 1);
        }
    }
}