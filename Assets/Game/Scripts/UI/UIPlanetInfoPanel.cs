using Game.Scripts.Money;
using Game.Scripts.Money.Upgrades;
using Game.Scripts.StarSystem.Planets;
using TMPro;
using UnityEngine;

namespace Game.Scripts.UI
{
    public class UIPlanetInfoPanel : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI incomePerMinuteText;
        [SerializeField] private UIBuyButton uiBuySatelliteButton;
        [SerializeField] private UpgradeView orbitSpeedUpgrade;
        [SerializeField] private UpgradeView axisSpeedUpgrade;
        [SerializeField] private UpgradeView incomeUpgrade;


        private void Start()
        {
            PlanetSelector.SelectedObjectChangedEvent += UpdateWindow;
            Wallet.MoneyChangedEvent += UpdateWindow;

            uiBuySatelliteButton.Subscribe(() =>
            {
                double nextSatellitePrice = StaticData.Price.NextSatellitePrice(
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
            uiBuySatelliteButton.gameObject.SetActive(canCreateSatellite);
            if (canCreateSatellite)
                UpdatePriceButton();

            UpdateUpgrades();
            UpdateIncomeText();
        }

        private void UpdateIncomeText() => incomePerMinuteText.text = $"{(PlanetSelector.SelectedBody.IncomePerSecond * 60).ToShortString()}/min";

        private void UpdateUpgrades()
        {
            var selectedBody = PlanetSelector.SelectedBody;
            if (selectedBody is Planet planet)
            {
                var upgradeData = planet.UpgradeData as PlanetUpgradeData;
                orbitSpeedUpgrade.UpdateCell(new UpgradeView.Protocol
                {
                    Upgrade = upgradeData.OrbitSpeedUpgrade
                });
                orbitSpeedUpgrade.gameObject.SetActive(true);
            }
            else
                orbitSpeedUpgrade.gameObject.SetActive(false);

            axisSpeedUpgrade.UpdateCell(new UpgradeView.Protocol
            {
                Upgrade = selectedBody.UpgradeData.AxisSpeedUpgrade
            });

            incomeUpgrade.UpdateCell(new UpgradeView.Protocol
            {
                Upgrade = selectedBody.UpgradeData.IncomeUpgrade
            });
        }

        private void UpdatePriceButton()
        {
            double nextSatellitePrice = StaticData.Price.NextSatellitePrice(PlanetSelector.SelectedBody.Depth, PlanetSelector.SelectedBody.SatellitesCount);
            uiBuySatelliteButton.UpdatePrice(nextSatellitePrice, Wallet.CurrentMoney >= nextSatellitePrice);
        }

        private void OnDestroy()
        {
            PlanetSelector.SelectedObjectChangedEvent -= UpdateWindow;
        }
    }
}