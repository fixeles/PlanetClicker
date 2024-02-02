using FPS.LocalizationService;
using Game.Scripts.Space.Common;
using TMPro;
using UnityEngine;

namespace Game.Scripts.UI
{
    public class UITotalMoneyCounter : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI counter;

        private void UpdateCounter() => counter.text = $"<sprite name=\"coin\">{(SpaceBody.TotalIncomePerSecond * 60).ToShortString()}/{Localization.Get("min")}";

        private void OnEnable()
        {
            UpdateCounter();
        }
    }
}