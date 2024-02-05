using System.Text;
using Game.Scripts.Money;
using TMPro;
using UnityEngine;

namespace Game.Scripts.UI
{
    public class UICurrencyView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI currencyCounter;

        private readonly StringBuilder _stringBuilder = new();

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
            _stringBuilder.Append("<sprite name=\"coin\">");
            _stringBuilder.Append(Wallet.CurrentMoney.ToShortString());
            currencyCounter.text = _stringBuilder.ToString();
            _stringBuilder.Clear();
        }
    }
}