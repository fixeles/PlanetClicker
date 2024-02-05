using System.Text;
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
        [SerializeField] private GameObject planetIcon;
        [SerializeField] private UIBuyButton uiBuySatelliteButton;
        [SerializeField] private UpgradeView orbitSpeedUpgrade;
        [SerializeField] private UpgradeView axisSpeedUpgrade;
        [SerializeField] private UpgradeView incomeUpgrade;

        private static readonly StringBuilder StringBuilder = new();

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
            planetIcon.SetActive(canCreateSatellite);
            if (canCreateSatellite)
                UpdatePriceButton();

            UpdateUpgrades();
        }

        private void UpdateUpgrades()
        {
            var selectedBody = PlanetSelector.SelectedBody;
            var localizedMinutes = string.Empty;

            if (selectedBody is Planet planet)
            {
                var upgradeData = planet.UpgradeData as PlanetUpgradeData;

                StringBuilder.Append((planet.PlanetMotionData.OrbitPerSecond * 60).ToString(Constants.Format));
                StringBuilder.Append(Constants.Slash);
                StringBuilder.Append(localizedMinutes);
                StringBuilder.Append(Constants.Arrow);
                StringBuilder.Append(Constants.NewLine);
                StringBuilder.Append((planet.PlanetMotionData.NextUpgradeOrbitPerSecond * 60).ToString(Constants.Format));
                StringBuilder.Append(Constants.Slash);
                StringBuilder.Append(localizedMinutes);

                orbitSpeedUpgrade.UpdateCell(new UpgradeView.Protocol
                {
                    Upgrade = upgradeData!.OrbitSpeedUpgrade,
                    Description = StringBuilder.ToString()
                });
                StringBuilder.Clear();
                orbitSpeedUpgrade.gameObject.TrySetActive(true);
            }
            else
                orbitSpeedUpgrade.gameObject.TrySetActive(false);

            StringBuilder.Append((selectedBody.MotionData.AxisPerSecondProp * 60).ToString(Constants.Format));
            StringBuilder.Append(Constants.Slash);
            StringBuilder.Append(localizedMinutes);
            StringBuilder.Append(Constants.Arrow);
            StringBuilder.Append(Constants.NewLine);
            StringBuilder.Append((selectedBody.MotionData.NextUpgradeAxisPerSecond * 60).ToString(Constants.Format));
            StringBuilder.Append(Constants.Slash);
            StringBuilder.Append(localizedMinutes);

            axisSpeedUpgrade.UpdateCell(new UpgradeView.Protocol
            {
                Upgrade = selectedBody.UpgradeData.AxisSpeedUpgrade,
                Description = StringBuilder.ToString()
            });
            StringBuilder.Clear();

            StringBuilder.Append((selectedBody.IncomePerSecond * 60).ToString(Constants.Format));
            StringBuilder.Append(Constants.Slash);
            StringBuilder.Append(localizedMinutes);
            StringBuilder.Append(Constants.Arrow);
            StringBuilder.Append(Constants.NewLine);
            StringBuilder.Append((selectedBody.NextUpgradeIncomePerSecond * 60).ToString(Constants.Format));
            StringBuilder.Append(Constants.Slash);
            StringBuilder.Append(localizedMinutes);

            incomeUpgrade.UpdateCell(new UpgradeView.Protocol
            {
                Upgrade = selectedBody.UpgradeData.IncomeUpgrade,
                Description = StringBuilder.ToString()
            });
            StringBuilder.Clear();
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

        private static class Constants
        {
            public const string Slash = "";
            public const string Arrow = "=>";
            public const string NewLine = "\n";
            public const string Format = "0.0";
        }
    }
}