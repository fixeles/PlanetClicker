using Game.Scripts.Money;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Scripts.UI
{
    public class UIPlanetInfoPanel : MonoBehaviour
    {
        [SerializeField] private Button buySatelliteButton;
        [SerializeField] private TextMeshProUGUI nextSatellitePriceText;


        private void Start()
        {
            PlanetSelector.SelectedObjectChangedEvent += UpdateWindow;
            Wallet.MoneyChangedEvent += UpdateWindow;
            
            buySatelliteButton.onClick.AddListener(() =>
            {
                double nextSatellitePrice = PriceCalculator.NextSatellitePrice(
                    PlanetSelector.SelectedBody.Depth,
                    PlanetSelector.SelectedBody.SatellitesCount);

                Wallet.TrySubtractMoneyWithCallback(
                    nextSatellitePrice,
                    () =>
                    {
                        PlanetSelector.SelectedBody.CreateSatellite();
                    });
            });
        }

        private void UpdateWindow()
        {
            bool canCreateSatellite = PlanetSelector.SelectedBody.CanCreateSatellite;
            buySatelliteButton.gameObject.SetActive(canCreateSatellite);
            if (canCreateSatellite)
                UpdatePriceButton();
        }

        private void UpdatePriceButton()
        {
            double nextSatellitePrice = PriceCalculator.NextSatellitePrice(PlanetSelector.SelectedBody.Depth, PlanetSelector.SelectedBody.SatellitesCount);
            nextSatellitePriceText.text = nextSatellitePrice.ToShortString();
            buySatelliteButton.interactable = Wallet.CurrentMoney >= nextSatellitePrice;
            nextSatellitePriceText.color = Wallet.CurrentMoney >= nextSatellitePrice ? Color.green : Color.red;
        }

        private void OnDestroy()
        {
            PlanetSelector.SelectedObjectChangedEvent -= UpdateWindow;
        }
    }
}