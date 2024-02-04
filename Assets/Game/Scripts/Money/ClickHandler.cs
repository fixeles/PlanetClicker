using System.Collections.Generic;
using System.Text;
using FPS.Pool;
using Game.Scripts.Core;
using Game.Scripts.UI;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Game.Scripts.Money
{
    public class ClickHandler : MonoBehaviour
    {
        [SerializeField] private GraphicRaycaster raycaster;
        [SerializeField] private ClickFX clickFX;

        private readonly StringBuilder _stringBuilder = new();

        private void Update()
        {
            if (!Input.GetMouseButtonDown(0))
                return;

            if (IsCursorOverUI())
                return;


            var reward = StaticData.Income.PerClick/* + 2 * (Wallet.CurrentMoney + 1)*/;
            Wallet.AddMoney(reward);
            
            _stringBuilder.Clear();
            _stringBuilder.Append("+");
            _stringBuilder.Append(reward.ToShortString());
            
            FluffyPool.Get<UIFloatingText>().ConfigureForClick(_stringBuilder.ToString());
            clickFX.Play();
        }

        private bool IsCursorOverUI()
        {
            var mousePosition = Input.mousePosition;
            var eventData = new PointerEventData(null)
            {
                position = mousePosition
            };

            var results = new List<RaycastResult>();
            raycaster.Raycast(eventData, results);
            for (int i = 0; i < results.Count; i++)
            {
                if (results[i].gameObject.transform is RectTransform)
                    return true;
            }

            return false;
        }
    }
}