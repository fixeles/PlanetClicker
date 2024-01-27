using Game.Scripts.Money;
using TMPro;
using UnityEngine;

namespace Game.Scripts.UI
{
    public class CurrencyView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI currencyCounter;

        private void Start()
        {
            Wallet.MoneyChangedEvent += UpdateCounter;
        }

        private void OnDestroy()
        {
            Wallet.MoneyChangedEvent -= UpdateCounter;
        }

        private void UpdateCounter()
        {
            currencyCounter.text = Wallet.CurrentMoney.ToString("0"); //todo write converter
        }
    }
}