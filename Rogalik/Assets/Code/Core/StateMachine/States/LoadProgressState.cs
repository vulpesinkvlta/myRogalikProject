using System;
using UnityEngine;

namespace Core
{
    public class LoadProgressState : IState
    {
        private readonly IGameStateMachine _stateMachine;

        public LoadProgressState(IGameStateMachine stateMachine,
            ISceneLoaderService sceneLoader)
        {
            _stateMachine = stateMachine;
        }


        public void Enter()
        {
            Debug.Log("LoadLevelState Enter");
            //progressService.LoadOrCreateProgress();
            _stateMachine.Enter<LoadSceneState, string>("1.MainMenu");
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
