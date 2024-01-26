using UnityEngine;
using UnityEngine.UI;

namespace Game.Scripts.UI
{
    public class PlanetInfoWindow : MonoBehaviour
    {
        // public static event Action<SpaceBody> CreateSatelliteRequest;
        [SerializeField] private Button buySatelliteButton;

        private void Start()
        {
            buySatelliteButton.onClick.AddListener(() =>
            {
                PlanetSelector.SelectedBody.CreateSatellite();
            });
        }
    }
}