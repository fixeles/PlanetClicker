using UnityEngine;
using UnityEngine.UI;

namespace Game.Scripts.UI
{
    public class PlanetSelectorPanel : MonoBehaviour
    {
        [SerializeField] private Button nextButton;
        [SerializeField] private Button prevButton;

        private void Start()
        {
            nextButton.onClick.AddListener(PlanetSelector.SelectNext);
            prevButton.onClick.AddListener(PlanetSelector.SelectPrev);
        }
    }
}