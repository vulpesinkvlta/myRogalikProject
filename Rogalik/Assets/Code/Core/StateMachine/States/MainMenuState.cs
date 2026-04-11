using UnityEngine;
using UnityEngine.SceneManagement;

namespace Core
{
    public class MainMenuState : IState
    {
        private readonly IGameStateMachine _stateMachine;

        public MainMenuState(IGameStateMachine stateMachine)
        {
            _stateMachine = stateMachine;
        }
        public void Enter()
        {
            Debug.Log("MainMenuState: Enter");
            SceneManager.LoadScene("1.MainMenu");
        }

        public void Exit()
        {
            Debug.Log("MainMenuState: Exit");
            //_stateMachine.Enter<GameplayState>(); 
        }
    }
}