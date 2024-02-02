using FPS.LocalizationService;
using Game.Scripts.Core;
using Game.Scripts.Money;
using Game.Scripts.Money.Upgrades;
using Game.Scripts.Space.Planets;
using UnityEngine;

namespace Game.Scripts.UI
{
    public class UIPlanetInfoPanel : MonoBehaviour
    {
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
            UpdateWindow();
        }

        private void UpdateWindow()
        {
            bool canCreateSatellite = PlanetSelector.SelectedBody.CanCreateSatellite;
            uiBuySatelliteButton.gameObject.SetActive(canCreateSatellite);
            if (canCreateSatellite)
                UpdatePriceButton();

            UpdateUpgrades();
        }

        private void UpdateUpgrades()
        {
            var selectedBody = PlanetSelector.SelectedBody;
            if (selectedBody is Planet planet)
            {
                var upgradeData = planet.UpgradeData as PlanetUpgradeData;
                orbitSpeedUpgrade.UpdateCell(new UpgradeView.Protocol
                {
                    Upgrade = upgradeData.OrbitSpeedUpgrade,
                    Description = $"{planet.PlanetMotionData.OrbitPerSecond * 60:0.0}/{Localization.Get("min")}=>\n{planet.PlanetMotionData.NextUpgradeOrbitPerSecond * 60:0.0}/{Localization.Get("min")}"
                });
                orbitSpeedUpgrade.gameObject.SetActive(true);
            }
            else
                orbitSpeedUpgrade.gameObject.SetActive(false);

            axisSpeedUpgrade.UpdateCell(new UpgradeView.Protocol
            {
                Upgrade = selectedBody.UpgradeData.AxisSpeedUpgrade,
                Description = $"{selectedBody.MotionData.AxisPerSecondProp * 60:0.0}/{Localization.Get("min")}=>\n{selectedBody.MotionData.NextUpgradeAxisPerSecond * 60:0.0}/{Localization.Get("min")}"
            });

            incomeUpgrade.UpdateCell(new UpgradeView.Protocol
            {
                Upgrade = selectedBody.UpgradeData.IncomeUpgrade,
                Description = $"{(selectedBody.IncomePerSecond * 60).ToShortString()}/{Localization.Get("min")}=>\n{(selectedBody.NextUpgradeIncomePerSecond * 60).ToShortString()}/{Localization.Get("min")}"
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