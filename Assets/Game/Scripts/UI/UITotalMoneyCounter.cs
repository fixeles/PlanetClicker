using Game.Scripts.StarSystem.Common;
using TMPro;
using UnityEngine;

namespace Game.Scripts.UI
{
    public class UITotalMoneyCounter : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI counter;

        private void UpdateCounter() => counter.text = $"{(SpaceBody.TotalIncomePerSecond * 60).ToShortString()}/min";

        private void OnEnable()
        {
            UpdateCounter();
        }
    }
}