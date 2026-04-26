using UnityEngine;

namespace Core
{
    public class StartRunState : IPayloadedState<string>
    {
        private readonly IGameStateMachine _gameStateMachine;
        private readonly ISceneLoaderService _sceneLoader;

        public StartRunState(IGameStateMachine gameStateMachine,
            ISceneLoaderService sceneLoader)
        {
            _gameStateMachine = gameStateMachine;
            _sceneLoader = sceneLoader;
        }

        public void Enter(string payload)
        {
            Debug.Log("Entered StartRunState");
            _sceneLoader.Load(payload, OnLoaded);
        }

        private void OnLoaded()
        {
            Debug.Log("StartRunState OnLoaded");
            _gameStateMachine.Enter<GenerateLevelState>();
        }

        public void Exit()
        {
            Debug.Log("Exited StartRunState");
        }
    }
}
