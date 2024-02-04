using System.Text;
using FPS.LocalizationService;
using Game.Scripts.Space.Common;
using TMPro;
using UnityEngine;

namespace Game.Scripts.UI
{
    public class UITotalMoneyCounter : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI counter;
        private readonly StringBuilder _stringBuilder = new();

        private void UpdateCounter()
        {
            _stringBuilder.Append("<sprite name=\"coin\">");
            _stringBuilder.Append((SpaceBody.TotalIncomePerSecond * 60).ToShortString());
            _stringBuilder.Append("/");
            _stringBuilder.Append(Localization.Get("min"));
            counter.text = _stringBuilder.ToString();
            _stringBuilder.Clear();
        }

        private void OnEnable()
        {
            UpdateCounter();
        }
    }
}