using System.Collections;
using FPS.Pool;
using TMPro;
using UnityEngine;

namespace Game.Scripts.UI
{
    public class UIFloatingText : MonoBehaviour
    {
        private static Camera _mainCamera;

        [SerializeField] private TextMeshProUGUI content;
        [SerializeField] private CanvasGroup canvasGroup;
        [SerializeField] private float upwardSpeed = 5;
        [SerializeField, Min(0.01f)] private float fadeTime = 1;
        [SerializeField, HideInInspector] private Transform cachedTransform;



        public void ConfigureForTarget(Transform followTarget, string text)
        {
            Init(text);
            StartCoroutine(FadeWithTargetRoutine(followTarget));
        }

        public void ConfigureForClick(string text)
        {
            Init(text);
            StartCoroutine(FadeAfterClickRoutine());
        }

        private void Init(string text)
        {
            content.text = text;
            canvasGroup.alpha = 1;
            FrequentCanvas.AddUIElement(cachedTransform);
        }

        private IEnumerator FadeWithTargetRoutine(Transform target)
        {
            float currentYOffset = 0;
            while (canvasGroup.alpha > 0)
            {
                canvasGroup.alpha -= Time.deltaTime / fadeTime;
                currentYOffset += Time.deltaTime * upwardSpeed;

                var uiPosition = _mainCamera.WorldToScreenPoint(target.position);
                uiPosition.y += currentYOffset;
                cachedTransform.position = uiPosition;
                yield return null;
            }

            FluffyPool.Return(this);
        }

        private IEnumerator FadeAfterClickRoutine()
        {
            var uiPosition = Input.mousePosition;
            while (canvasGroup.alpha > 0)
            {
                canvasGroup.alpha -= Time.deltaTime / fadeTime;
                uiPosition.y += Time.deltaTime * upwardSpeed;
                cachedTransform.position = uiPosition;
                yield return null;
            }

            FluffyPool.Return(this);
        }

        private void OnValidate()
        {
            cachedTransform ??= transform;
        }

        private void Awake()
        {
            _mainCamera ??= Camera.main;
        }
    }
}