using UnityEngine;

namespace Core
{
    public class LevelLoopState : IState
    {
        private readonly IGameStateMachine _gameStateMachine;
        private readonly ISceneLoaderService _sceneLoader;
        private readonly IEventBus _eventBus;

        public LevelLoopState(IGameStateMachine gameStateMachine,
            IEventBus eventBus)
        {
            _gameStateMachine = gameStateMachine;
            _eventBus = eventBus;
        }
        public void Enter()
        {
            Debug.Log("Entered LevelLoopState");
            _eventBus.Subscribe<PlayerDiedEvent>(OnPlayerDied);
        }

        public void Exit()
        {
            Debug.Log("Exited LevelLoopState");
            _eventBus.Unsubscribe<PlayerDiedEvent>(OnPlayerDied);
        }

        private void OnPlayerDied(PlayerDiedEvent evt)
        {
            _gameStateMachine.Enter<PlayerDeathState>();
        }
    }
}
