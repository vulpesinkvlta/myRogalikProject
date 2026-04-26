using UnityEngine;

namespace Core
{
    public class LevelLoopState : IState
    {
        private readonly IGameStateMachine _gameStateMachine;
        private readonly ISceneLoaderService _sceneLoader;

        public LevelLoopState(IGameStateMachine gameStateMachine)
        {
            _gameStateMachine = gameStateMachine;
        }
        public void Enter()
        {
            Debug.Log("Entered LevelLoopState");
        }

        public void Exit()
        {
            Debug.Log("Exited LevelLoopState");
        }
    }
}
