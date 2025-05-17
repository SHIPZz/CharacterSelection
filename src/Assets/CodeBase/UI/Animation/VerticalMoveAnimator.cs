using DG.Tweening;
using UnityEngine;

namespace CodeBase.UI.Animation
{
    public class VerticalMoveAnimator : MonoBehaviour
    {
        [SerializeField] private float _animationDuration = 0.3f;
        [SerializeField] private float _highlightOffset = 50f;
        [SerializeField] private Transform _targetTransform;
        
        private Vector3 _initialPosition;
        private Tweener _highlightTween;
        private Tweener _unhighlightTween;

        private void Awake()
        {
            _initialPosition = _targetTransform.localPosition;
            CreateTweens();
        }

        public void Raise()
        {
            _unhighlightTween.Pause();
            _highlightTween.Restart();
        }

        public void Down()
        {
            _highlightTween.Pause();
            _unhighlightTween.Restart();
        }

        private void OnDestroy()
        {
            _highlightTween?.Kill();
            _unhighlightTween?.Kill();
        }

        private void CreateTweens()
        {
            _highlightTween = _targetTransform
                .DOLocalMoveY(_initialPosition.y + _highlightOffset, _animationDuration)
                .SetEase(Ease.OutQuad)
                .SetAutoKill(false)
                .Pause();

            _unhighlightTween = _targetTransform
                .DOLocalMoveY(_initialPosition.y, _animationDuration)
                .SetEase(Ease.OutQuad)
                .SetAutoKill(false)
                .Pause();
        }
    }
} 