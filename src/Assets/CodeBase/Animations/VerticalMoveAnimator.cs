using DG.Tweening;
using UnityEngine;

namespace CodeBase.Animations
{
    public class VerticalMoveAnimator : MonoBehaviour
    {
        [SerializeField] private float _animationDuration = 0.3f;
        [SerializeField] private float _highlightOffset = 50f;
        [SerializeField] private Transform _targetTransform;
        
        private Vector3 _initialPosition;
        private Tweener _raiseTween;
        private Tweener _putDownTween;

        private void Awake()
        {
            _initialPosition = _targetTransform.localPosition;
            CreateTweens();
        }

        public void Raise()
        {
            _putDownTween.Pause();
            _raiseTween.Restart();
        }

        public void Down()
        {
            _raiseTween.Pause();
            _putDownTween.Restart();
        }

        private void OnDestroy()
        {
            _raiseTween?.Kill();
            _putDownTween?.Kill();
        }

        private void CreateTweens()
        {
            _raiseTween = _targetTransform
                .DOLocalMoveY(_initialPosition.y + _highlightOffset, _animationDuration)
                .SetEase(Ease.OutQuad)
                .SetAutoKill(false)
                .Pause();

            _putDownTween = _targetTransform
                .DOLocalMoveY(_initialPosition.y, _animationDuration)
                .SetEase(Ease.OutQuad)
                .SetAutoKill(false)
                .Pause();
        }
    }
} 