using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

namespace CodeBase.UI.Animation
{
    [RequireComponent(typeof(Image))]
    public class FillAmountAnimator : MonoBehaviour
    {
        [SerializeField] private float _duration = 0.5f;
        [SerializeField] private Ease _ease = Ease.OutQuad;
        
        private Image _image;
        private Tween _currentTween;

        private void Awake()
        {
            _image = GetComponent<Image>();
        }

        public void AnimateTo(float targetFillAmount)
        {
            StopCurrentAnimation();
            _currentTween = _image.DOFillAmount(targetFillAmount, _duration)
                .SetEase(_ease);
        }

        public void StopCurrentAnimation()
        {
            _currentTween?.Kill();
            _currentTween = null;
        }

        public void PauseAnimation()
        {
            _currentTween?.Pause();
        }

        public void ResumeAnimation()
        {
            _currentTween?.Play();
        }

        public void RestartAnimation()
        {
            _currentTween?.Restart();
        }

        private void OnDestroy()
        {
            StopCurrentAnimation();
        }
    }
} 