using UnityEngine;
using UnityEngine.SceneManagement;

namespace Core
{
    public class MainMenuState : IState
    {
        private readonly IGameStateMachine _stateMachine;
        private readonly ISceneLoaderService _sceneLoader;


        public MainMenuState(IGameStateMachine stateMachine)
        {
            _stateMachine = stateMachine;
        }
        public void Enter()
        {
            Debug.Log("MainMenuState: Enter");
        }

        public void Exit()
        {
            Debug.Log("MainMenuState: Exit");
        }
    }
}