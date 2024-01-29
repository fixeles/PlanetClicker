using System.Collections;
using Game.Scripts.Money.Upgrades;
using Game.Scripts.StarSystem.Common;
using TMPro;
using UnityEngine;

namespace Game.Scripts.UI
{
    public class TotalMoneyCounter : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI counter;

        private void UpdateCounter() => counter.text = $"{(SpaceBody.TotalIncomePerSecond * 60).ToShortString()}/min";

        private void Start()
        {
            SpaceBody.NewBodyCreatedEvent += UpdateCounter;
            Upgrade.AnyUpgradeEvent += UpdateCounter;
            UpdateCounter();
        }

        private void OnDestroy()
        {
            SpaceBody.NewBodyCreatedEvent -= UpdateCounter;
            Upgrade.AnyUpgradeEvent -= UpdateCounter;
        }
    }
}