using CodeBase.Infrastructure.States.StateInfrastructure;
using CodeBase.UI.Services.Window;
using CodeBase.UI.Victory;

namespace CodeBase.Infrastructure.States.States
{
    public class VictoryState : IState
    {
        private readonly IWindowService _windowService;

        public VictoryState(IWindowService windowService)
        {
            _windowService = windowService;
        }

        public void Enter()
        {
            _windowService.OpenWindowAsync<VictoryWindow>(true);
        }

        public void Exit() { }
    }
}