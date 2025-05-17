using CodeBase.Infrastructure.States.StateInfrastructure;
using CodeBase.UI.CharacterSelect;
using CodeBase.UI.CharacterSelect.Views;
using CodeBase.UI.Services.Window;

namespace CodeBase.Infrastructure.States.States
{
    public class CharacterSelectState : IState
    {
        private readonly IWindowService _windowService;

        public CharacterSelectState(IWindowService windowService)
        {
            _windowService = windowService;
        }

        public void Enter()
        {
            _windowService.OpenWindow<CharacterSelectWindow>();
        }

        public void Exit()
        {
            _windowService.Close<CharacterSelectWindow>();
        }
    }
}