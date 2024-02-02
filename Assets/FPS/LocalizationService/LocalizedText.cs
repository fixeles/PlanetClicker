using TMPro;
using UnityEngine;

namespace FPS.LocalizationService
{
    [RequireComponent(typeof(TextMeshProUGUI))]
    public class LocalizedText : MonoBehaviour
    {
        [SerializeField] private string key = "";
        [SerializeField, HideInInspector] private TextMeshProUGUI text;

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