using CodeBase.Infrastructure.States.StateMachine;
using CodeBase.Infrastructure.States.States;
using CodeBase.UI.CharacterSelect.Services;
using CodeBase.UI.CharacterSelect.Views;
using CodeBase.UI.Controllers;
using CodeBase.UI.Services.Window;
using UniRx;

namespace CodeBase.UI.CharacterSelect.Controllers
{
    public class CharacterSelectWindowController : IController<CharacterSelectWindow>
    {
        private readonly IWindowService _windowService;
        private readonly IStateMachine _stateMachine;
        private readonly ICharacterSelectionService _characterService;
        private readonly CompositeDisposable _disposables = new();

        private CharacterSelectWindow _window;

        public CharacterSelectWindowController(
            IWindowService windowService,
            IStateMachine stateMachine,
            ICharacterSelectionService characterService)
        {
            _stateMachine = stateMachine;
            _windowService = windowService;
            _characterService = characterService;
        }

        public void Initialize()
        {
            _window.OnPreviousCharacterClicked
                .Subscribe(_ => OnPreviousCharacterClicked())
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

            _windowService.OpenWindowInParent<CharacterPanelView>(_window.CharacterPanelViewParent);
        }

        public void BindView(CharacterSelectWindow window) => _window = window;

        public void Dispose()
        {
            _disposables.Dispose();
            
            _windowService.Close<CharacterPanelView>();
        }

        private void OnPreviousCharacterClicked() => _characterService.SwitchToPreviousCharacter();

        private void OnNextCharacterClicked() => _characterService.SwitchToNextCharacter();

        private void OnBackToMenuClicked() =>
            _windowService.Close<CharacterSelectWindow>(() => _stateMachine.Enter<LoadingMenuState>());
    }
}