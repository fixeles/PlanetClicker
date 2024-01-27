using UnityEngine;
using UnityEngine.UI;

namespace Game.Scripts.UI
{
    public class PlanetInfoWindow : MonoBehaviour
    {
        // public static event Action<SpaceBody> CreateSatelliteRequest;
        [SerializeField] private Button buySatelliteButton;

        private void UpdateWindow()
        {
            buySatelliteButton.gameObject.SetActive(PlanetSelector.SelectedBody.CanCreateSatellite);
        }

        private void Start()
        {
            PlanetSelector.SelectedObjectChangedEvent += UpdateWindow;
            buySatelliteButton.onClick.AddListener(() =>
            {
                PlanetSelector.SelectedBody.CreateSatellite();
                UpdateWindow();
            });
        }

        private void OnDestroy()
        {
            PlanetSelector.SelectedObjectChangedEvent -= UpdateWindow;
        }
    }
}