using TMPro;
using UnityEngine;

namespace FPS.LocalizationService
{
    [RequireComponent(typeof(TextMeshProUGUI))]
    public class LocalizedText : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI text;
        [SerializeField] private string key;

        private void OnEnable()
        {
            text.text = Localization.Get(key);
        }

        private void OnValidate()
        {
            text ??= GetComponent<TextMeshProUGUI>();
        }
    }
}