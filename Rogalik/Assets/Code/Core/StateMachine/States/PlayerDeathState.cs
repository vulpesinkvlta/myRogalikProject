
using UnityEngine;

namespace Core
{
    public class PlayerDeathState : IState
    {
        private readonly IGameStateMachine _gameStateMachine;

        public PlayerDeathState(IGameStateMachine gameStateMachine)
        {
            _gameStateMachine = gameStateMachine;
        }
        public void Enter()
        {
            Debug.Log("Entered PlayerDeathState");
                
            Time.timeScale = 0f;
            // открыть DeathChoice UI
            // выбор:
            // 1. Continue to next level with sin
            // 2. Retry current level
        }

        private void OnContinueChosen()
        {
            // _gameStateMachine.Enter<LevelLoopState>();
        }

        private void OnRetryChosen()
        {
            // _gameStateMachine.Enter<LevelLoopState>();
        }

        public void Exit()
        {
            Debug.Log("Exited PlayerDeathState");
            Time.timeScale = 1f;
            //close DeathChoice UI  
        }
    }
}
