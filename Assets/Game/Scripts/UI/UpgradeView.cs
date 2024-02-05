using Game.Scripts.Core;
using Game.Scripts.Money;
using Game.Scripts.Money.Upgrades;
using TMPro;
using UnityEngine;

namespace Game.Scripts.UI
{
    public class UpgradeView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI description;
        [SerializeField] private UIBuyButton upgradeBtn;

        public void UpdateCell(Protocol protocol)
        {
            upgradeBtn.ClearSubscriptions();
            int selectedBodyDepth = PlanetSelector.SelectedBody.Depth;
            double nextUpgradePrice = StaticData.Price.NextUpgradePrice(selectedBodyDepth, protocol.Upgrade.Level);

            description.text = protocol.Description;

            upgradeBtn.UpdatePrice(nextUpgradePrice, Wallet.HasMoney(nextUpgradePrice));
            upgradeBtn.Subscribe(() =>
            {
                Wallet.TrySubtractMoneyWithCallback(nextUpgradePrice, () => protocol.Upgrade.AddLevel());
            });
        }

        public struct Protocol
        {
            public Upgrade Upgrade;
            public string Description;
        }
    }
}