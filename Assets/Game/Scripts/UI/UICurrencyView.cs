using Game.Scripts.Money;
using TMPro;
using UnityEngine;

namespace Game.Scripts.UI
{
    public class UICurrencyView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI currencyCounter;

        private void Start()
        {
            Wallet.MoneyChangedEvent += UpdateCounter;
            UpdateCounter();
        }

        private void OnDestroy()
        {
            Wallet.MoneyChangedEvent -= UpdateCounter;
        }

        private void UpdateCounter()
        {
            currencyCounter.text =$"<sprite name=\"coin\">{Wallet.CurrentMoney.ToShortString()}";
        }
    }
}