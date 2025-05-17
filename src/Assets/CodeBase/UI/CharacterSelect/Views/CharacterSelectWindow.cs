using System;
using System.Collections.Generic;
using CodeBase.Animations;
using CodeBase.UI.AbstractWindow;
using CodeBase.UI.CharacterSelect.Configs;
using CodeBase.UI.CharacterSelect.Enums;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.UI.CharacterSelect.Views
{
    public class CharacterSelectWindow : AbstractWindowBase
    {
        [Header("Character Selection")]
        [SerializeField] private Button _previousCharacterButton;
        [SerializeField] private Button _nextCharacterButton;
        [SerializeField] private Button _backToMenuButton;
        [SerializeField] private CharacterView _mainSelectedCharacter;
        [SerializeField] private CharacterPanelView _characterPanelView;
        
        [Header("Animation Settings")]
        [SerializeField] private float _characterSwitchDuration = 0.5f;
        
       [SerializeField] private ScaleAnimator _windowScaleAnimator;
       [SerializeField] private ScaleAnimator _characterScaleAnimator;
       
       public Transform CharacterLayout => _characterPanelView.CharacterLayout;

       public IObservable<Unit> OnPreviousCharacterClicked => _previousCharacterButton.OnClickAsObservable();
        public IObservable<Unit> OnNextCharacterClicked => _nextCharacterButton.OnClickAsObservable();
        public IObservable<Unit> OnBackToMenuClicked => _backToMenuButton.OnClickAsObservable();

        protected override void OnOpenStarted()
        {
            transform.localScale = Vector3.zero;
        }

        protected override void OnOpen() => AnimateWindowShow();

        public void SetCharacters(IEnumerable<CharacterView> characters)
        {
            _characterPanelView.Add(characters);
        }

        public void SwitchCharacter(CharacterData character)
        {
            _characterScaleAnimator.AnimateToZero();
            
            _characterScaleAnimator.OnAnimationComplete(() =>
            {
                _mainSelectedCharacter.Init(character.TypeId,character.Icon,character.MainBackground, character.Progress);
                _characterScaleAnimator.AnimateToOne();
                RaiseCharacter(character.TypeId);
            });
        }

        private void RaiseCharacter(CharacterTypeId id) => _characterPanelView.RaiseCharacter(id);

        private void AnimateWindowShow() => _windowScaleAnimator.AnimateToOne();
    }
} 