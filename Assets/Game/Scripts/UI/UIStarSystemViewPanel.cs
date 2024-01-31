using System.Collections;
using Cinemachine;
using Game.Scripts.Core;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Scripts.UI
{
    public class UIStarSystemViewPanel : MonoBehaviour
    {
        [SerializeField] private Button nextButton;
        [SerializeField] private Button prevButton;
        [SerializeField] private Button switchCameraBtn;
        [SerializeField] private CinemachineVirtualCamera planetCamera;
        [SerializeField] private CinemachineVirtualCamera systemCamera;
        [SerializeField] private GameObject planetInfoPanel;
        [SerializeField] private GameObject totalMoneyCounter;


        private bool _isPlanetViewState = true;


        private void Start()
        {
            nextButton.onClick.AddListener(PlanetSelector.SelectNext);
            prevButton.onClick.AddListener(PlanetSelector.SelectPrev);

            UpdateViewState(0.01f);
            switchCameraBtn.onClick.AddListener(() =>
            {
                _isPlanetViewState = !_isPlanetViewState;
                UpdateViewState();
            });
        }

        private void UpdateViewState(float waitForCameraMove = 2)
        {
            StopAllCoroutines();
            systemCamera.enabled = !_isPlanetViewState;
            planetCamera.enabled = _isPlanetViewState;
            planetInfoPanel.SetActive(_isPlanetViewState);
            prevButton.gameObject.SetActive(_isPlanetViewState);
            nextButton.gameObject.SetActive(_isPlanetViewState);
            totalMoneyCounter.SetActive(!_isPlanetViewState);

            if (!_isPlanetViewState)
                return;

            StartCoroutine(ConfigPlanetViewCameraRoutine(waitForCameraMove));
        }

        private IEnumerator ConfigPlanetViewCameraRoutine(float waitTime)
        {
            yield return new WaitForSeconds(waitTime);
            var orbitalComponent = planetCamera.GetCinemachineComponent<CinemachineOrbitalTransposer>();
            var x = orbitalComponent.m_XAxis;
            x.m_InputAxisValue = 0.05f;
            orbitalComponent.m_XAxis = x;
            orbitalComponent.UpdateInputAxisProvider();
        }
    }
}