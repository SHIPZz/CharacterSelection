using System.Collections.Generic;
using CodeBase.UI.CharacterSelect.Configs;
using CodeBase.UI.CharacterSelect.Factory;
using CodeBase.UI.CharacterSelect.Services;
using CodeBase.UI.CharacterSelect.Views;
using CodeBase.UI.Controllers;
using UniRx;
using UnityEngine.Pool;

namespace CodeBase.UI.CharacterSelect.Controllers
{
    public class CharacterPanelController : IController<CharacterPanelView>
    {
        private readonly ICharacterSelectionService _characterService;
        private readonly ICharacterUIFactory _characterUIFactory;
        private readonly CompositeDisposable _disposables = new();

        private CharacterPanelView _window;

        public CharacterPanelController(ICharacterSelectionService characterService,
            ICharacterUIFactory characterUIFactory)
        {
            _characterService = characterService;
            _characterUIFactory = characterUIFactory;
        }

        public void Initialize()
        {
            _characterService.CurrentCharacter
                .Subscribe(character => _window.RaiseCharacter(character.TypeId))
                .AddTo(_disposables);

            _window
                .OnOpenStartedEvent
                .Subscribe(_ => SetupWindow())
                .AddTo(_disposables);

            _window
                .OnCharacterSelected
                .Subscribe(characterId => _characterService.SetCharacter(characterId))
                .AddTo(_disposables);
        }

        public void BindView(CharacterPanelView window) => _window = window;

        public void Dispose() => _disposables.Dispose();

        private void CreateCharacters()
        {
            using (ListPool<CharacterView>.Get(out List<CharacterView> characterViews))
            {
                foreach (CharacterData characterData in _characterService.Characters)
                {
                    CharacterView createdView =
                        _characterUIFactory.CreateCharacterView(_window.CharacterLayout, characterData);
                    characterViews.Add(createdView);
                }

                _window.SetCharacters(characterViews);
            }
        }

        private void SetupWindow()
        {
            CreateCharacters();

            _window.ForceLayoutRebuild();
            _window.RaiseCharacter(_characterService.CurrentCharacter.Value.TypeId);
        }
    }
}