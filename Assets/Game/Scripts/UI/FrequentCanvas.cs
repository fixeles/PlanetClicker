using UnityEngine;

namespace Game.Scripts.UI
{
    public class FrequentCanvas : MonoBehaviour
    {
        private static FrequentCanvas _instance;

        public static void AddUIElement(Transform element) => element.SetParent(_instance.transform);

        private void Awake()
        {
            _instance = this;
        }
    }
}