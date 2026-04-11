using System;
using UnityEngine;

namespace Core
{
    public class LoadProgressState : IPayloadedState<string>
    {
        private readonly IGameStateMachine _stateMachine;
        private readonly ISceneLoaderService _sceneLoader;

        public LoadProgressState(IGameStateMachine stateMachine,
            ISceneLoaderService sceneLoader)
        {
            _stateMachine = stateMachine;
            _sceneLoader = sceneLoader;
        }
        public void Enter(string sceneName)
        {
            Debug.Log($"Loading level: Enter");
            _sceneLoader.Load(sceneName, OnLoaded);
        }

        private void OnLoaded()
        {
            // _gameFactory.CreatePlayer(levelData.playerPosition);
            //  _saveLoad.Load();
            _stateMachine.Enter<MainMenuState>();
            //_curtain.Hide();
        }

        public void Exit()
        {
            //_saveLoad.Save();
            //_saveLoad.Cleanup();
            //_gameContext.Cleanup();
            Debug.Log("LoadLevelState Exit");
        }
    }
}
