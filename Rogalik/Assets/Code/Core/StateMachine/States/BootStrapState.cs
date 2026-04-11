using UnityEngine;

namespace Core
{
    public class BootStrapState : IState
    {
        private readonly IGameStateMachine _stateMachine;

        public BootStrapState(IGameStateMachine stateMachine)
        {
            _stateMachine = stateMachine;
        }
        public void Enter()
        {
            Debug.Log("BootStrapState: Enter");
            Debug.Log("Loading Curtain");
            Debug.Log("Loading Resources");
            //_curtain.Show();
            //_config.Load();
            _stateMachine.Enter<LoadLevelState, string>("1.MainMenu");
        }

        public void Exit()
        {
            Debug.Log("BootstrapState : Exit");
        }
    }
}
