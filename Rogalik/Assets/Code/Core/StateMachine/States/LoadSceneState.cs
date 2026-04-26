using System;
using UnityEngine;

namespace Core
{
    public class LoadSceneState : IPayloadedState<string>
    {
        private readonly IGameStateMachine _gameStateMachine;
        private readonly ISceneLoaderService _sceneLoader;

        public LoadSceneState(IGameStateMachine gameStateMachine, ISceneLoaderService sceneLoader)
        {
            _gameStateMachine = gameStateMachine;
            _sceneLoader = sceneLoader;
        }
        public void Enter(string payload)
        {
            Debug.Log("LoadSceneState Enter");
            _sceneLoader.Load(payload, OnLoaded);
        }

        private void OnLoaded()
        {
            _gameStateMachine.Enter<MainMenuState>();
        }

        public void Exit()
        {
            Debug.Log("LoadSceneState Exit");
        }
    }
}