
using UnityEngine;

namespace Core
{
    public class LevelCompleteState : IState
    {
        private readonly IGameStateMachine _gameStateMachine;

        public LevelCompleteState(IGameStateMachine gameStateMachine)
        {
            _gameStateMachine = gameStateMachine;
        }
        public void Enter()
        {
            Debug.Log("Entered LevelCompleteState");
            // Позже здесь будет:
            // - проверка результата греха
            // - запись прогресса
            // - переход на следующий уровень
            // - или финальная концовка
        }

        public void Exit()
        {
            Debug.Log("Exited LevelCompleteState");
        }
    }
}
