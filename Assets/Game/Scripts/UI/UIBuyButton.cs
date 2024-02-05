using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Game.Scripts.UI
{
    [RequireComponent(typeof(Button))]
    public class UIBuyButton : MonoBehaviour
    {
        [SerializeField, HideInInspector] private Button button;
        [SerializeField] private TextMeshProUGUI priceText;

        public void Subscribe(UnityAction action) => button.onClick.AddListener(action);

        public void UpdatePrice(double price, bool hasMoney)
        {
            button.interactable = hasMoney;
            priceText.text = price.ToShortString();
            priceText.color = hasMoney ? Color.white : Color.red;
        }

        public void ClearSubscriptions() => button.onClick.RemoveAllListeners();

        private void OnValidate()
        {
            button ??= GetComponent<Button>();
        }
    }
}