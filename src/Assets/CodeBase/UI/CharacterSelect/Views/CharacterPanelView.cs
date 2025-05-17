using System.Collections.Generic;
using System.Linq;
using CodeBase.UI.CharacterSelect.Enums;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.UI.CharacterSelect.Views
{
    public class CharacterPanelView : MonoBehaviour
    {
        [SerializeField] private List<CharacterView> _characterViews;
        [SerializeField] private Transform _characterLayout;
        [SerializeField] private ScrollRect _scrollRect;
        [SerializeField] private float _scrollDuration = 0.3f;
        [SerializeField] private float _initialXPosition = 260f;

        private CharacterView _currentCharacter;
        private RectTransform _scrollContent;
        private Tweener _scrollTweener; 

        public Transform CharacterLayout => _characterLayout;

        private void Awake()
        {
            _scrollContent = _scrollRect.content;
            _scrollContent.anchoredPosition = new Vector2(_initialXPosition, _scrollContent.anchoredPosition.y);
        }

        private void OnDestroy()
        {
            _scrollTweener?.Kill();
        }

        public void Add(IEnumerable<CharacterView> characterViews) => _characterViews.AddRange(characterViews);

        public void RaiseCharacter(CharacterTypeId id)
        {
            _currentCharacter?.PutDown();

            _currentCharacter = _characterViews.FirstOrDefault(x => x.Id == id);

            _currentCharacter?.Raise();
            
            ScrollToCharacter(_currentCharacter);
        }

        private void ScrollToCharacter(CharacterView character)
        {
            if(character == null)
                return;
            
            Vector3 characterPosition = character.RectTransform.position;
            Vector3 viewportPosition = _scrollRect.viewport.position;

            bool isVisible = RectTransformUtility.RectangleContainsScreenPoint(
                _scrollRect.viewport,
                characterPosition,
                null);

            if (!isVisible)
                MoveScrollToSeeItem(characterPosition, viewportPosition);
        }

        private void MoveScrollToSeeItem(Vector3 characterPosition, Vector3 viewportPosition)
        {
            _scrollTweener?.Kill();
            
            float targetX = _scrollContent.anchoredPosition.x - (characterPosition.x - viewportPosition.x);

            _scrollTweener = DOTween.To(
                    () => _scrollContent.anchoredPosition.x,
                    x => _scrollContent.anchoredPosition = new Vector2(x, _scrollContent.anchoredPosition.y),
                    targetX,
                    _scrollDuration)
                .SetEase(Ease.OutQuad)
                .OnKill(() => _scrollTweener = null);
        }
    }
}