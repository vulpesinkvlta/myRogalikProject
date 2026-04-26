using System;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Core
{
    public class MenuSceneButton : MonoBehaviour
    {
        private IGameStateMachine _gameStateMachine;

        [Inject]
        public void Construct(IGameStateMachine gameStateMachine)
        {
            _gameStateMachine = gameStateMachine;
        }

        public void StartGame()
        {
            _gameStateMachine.Enter<StartRunState, string>("2.Game");
        }
    }
}
