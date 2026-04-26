
using UnityEngine;

namespace Core
{
    public class GenerateLevelState : IState
    {
        private readonly IGameStateMachine _gameStateMachine;

        public GenerateLevelState(IGameStateMachine gameStateMachine)
        {
            _gameStateMachine = gameStateMachine;
        }
        public void Enter()
        {
            Debug.Log("Entered GenerateLevelState");
            _gameStateMachine.Enter<LevelLoopState>();
        }

        public void Exit()
        {
            Debug.Log("Exited GenerateLevelState"); 
        }
    }
}
