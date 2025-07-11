using System;
using CodeBase.Infrastructure.Loading;
using CodeBase.Infrastructure.States.StateInfrastructure;
using CodeBase.Infrastructure.States.StateMachine;
using CodeBase.UI.LoadingCurtains;
using CodeBase.UI.Services.Window;

namespace CodeBase.Infrastructure.States.States
{
    public class LoadingMenuState : IState
    {
        private readonly ISceneLoader _sceneLoader;
        private readonly IStateMachine _stateMachine;
        private readonly IWindowService _windowService;

        public LoadingMenuState(ISceneLoader sceneLoader, IStateMachine stateMachine, IWindowService windowService)
        {
            _stateMachine = stateMachine ?? throw new ArgumentNullException(nameof(stateMachine));
            _windowService = windowService;
            _sceneLoader = sceneLoader ?? throw new ArgumentNullException(nameof(sceneLoader));
        }

        public void Enter()
        {
            _sceneLoader.LoadScene(Scenes.Menu, () =>_stateMachine.Enter<MenuState>());
        }

        public void Exit()
        {
            
        }
    }
}