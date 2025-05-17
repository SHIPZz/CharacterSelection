using System.Collections.Generic;
using CodeBase.Infrastructure.States.StateMachine;
using CodeBase.Infrastructure.States.States;
using CodeBase.UI.CharacterSelect.Configs;
using CodeBase.UI.CharacterSelect.Factory;
using CodeBase.UI.CharacterSelect.Services;
using CodeBase.UI.CharacterSelect.Views;
using CodeBase.UI.Controllers;
using CodeBase.UI.Services.Window;
using UniRx;
using UnityEngine.Pool;

namespace CodeBase.UI.CharacterSelect.Controllers
{
    public class CharacterSelectWindowController : IController<CharacterSelectWindow>
    {
        private readonly IWindowService _windowService;
        private readonly IStateMachine _stateMachine;
        private readonly ICharacterSelectionService _characterService;
        private readonly ICharacterUIFactory _characterUIFactory;
        private readonly CompositeDisposable _disposables = new();

        private CharacterSelectWindow _window;

        public CharacterSelectWindowController(
            IWindowService windowService,
            IStateMachine stateMachine,
            ICharacterSelectionService characterService,
            ICharacterUIFactory characterUIFactory)
        {
            _stateMachine = stateMachine;
            _windowService = windowService;
            _characterService = characterService;
            _characterUIFactory = characterUIFactory;
        }

        public void Initialize()
        {
            _window.OnPreviousCharacterClicked
                .Subscribe(_ => OnPreviousCharacterClicked())
                .AddTo(_disposables);

            _window
                .OnOpenStartedEvent
                .Subscribe(_ => CreateCharacters())
                .AddTo(_disposables);

            _window.OnNextCharacterClicked
                .Subscribe(_ => OnNextCharacterClicked())
                .AddTo(_disposables);

            _window.OnBackToMenuClicked
                .Subscribe(_ => OnBackToMenuClicked())
                .AddTo(_disposables);

            _characterService.CurrentCharacter
                .Subscribe(character => _window.SwitchCharacter(character))
                .AddTo(_disposables);

            _window.SwitchCharacter(_characterService.CurrentCharacter.Value);
        }

        private void CreateCharacters()
        {
            using (ListPool<CharacterView>.Get(out List<CharacterView> characterViews))
            {
                foreach (CharacterData characterData in _characterService.Characters)
                {
                    CharacterView createdView = _characterUIFactory.CreateCharacterView(_window.CharacterLayout, characterData);
                    characterViews.Add(createdView);
                }

                _window.SetCharacters(characterViews);
            }
        }

        public void BindView(CharacterSelectWindow window) => _window = window;

        public void Dispose() => _disposables.Dispose();

        private void OnPreviousCharacterClicked() => _characterService.SwitchToPreviousCharacter();

        private void OnNextCharacterClicked() => _characterService.SwitchToNextCharacter();

        private void OnBackToMenuClicked() =>
            _windowService.Close<CharacterSelectWindow>(() => _stateMachine.Enter<LoadingMenuState>());
    }
}