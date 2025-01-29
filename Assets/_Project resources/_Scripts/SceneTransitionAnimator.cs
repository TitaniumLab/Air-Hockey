using DG.Tweening;
using DG.Tweening.Core;
using System.Threading.Tasks;
using UnityEngine;

namespace AirHockey
{
    [RequireComponent(typeof(CanvasGroup))]
    public class SceneTransitionAnimator : MonoBehaviour
    {
        [SerializeField] private float _delay = 0.5f;
        [SerializeField] private float _fadeDuration = 0.5f;
        private CanvasGroup _canvasGroup;

        private async void Awake()
        {
            _canvasGroup = GetComponent<CanvasGroup>();
            await PlayFadeOutAsync();
        }

        public void PlayInstant()
        {
            gameObject.SetActive(true);
        }

        public void StopInstant()
        {
            gameObject.SetActive(false);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fadeDuration">If the value is set to -1, the value is taken from the inspector</param>
        /// <param name="delay">If the value is set to -1, the value is taken from the inspector</param>
        /// <returns></returns>
        public async Task PlayFadeOutAsync(float fadeDuration = -1, float delay = -1)
        {
            fadeDuration = fadeDuration == -1 ? _fadeDuration : fadeDuration;
            delay = delay == -1 ? _delay : delay;
            var tween = _canvasGroup.DOFade(0, fadeDuration).From(1);
            tween.SetDelay(delay);
            await tween.AsyncWaitForCompletion();
        }
    }
}
