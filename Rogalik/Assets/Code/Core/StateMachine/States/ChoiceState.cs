using GamePlay;
using System;
using UnityEngine;

namespace Core
{
    public class ChoiceState : IPayloadedState<SinChoicePayload>
    {
        private readonly IGameStateMachine _gameStateMachine;
        private readonly IEventBus _eventBus;
        private readonly ISinChoiceUIService _choiceUIService;

        private SinChoicePayload _payload;

        public ChoiceState(IGameStateMachine gameStateMachine, IEventBus eventBus, ISinChoiceUIService choiceUIService)
        {
            _gameStateMachine = gameStateMachine;
            _eventBus = eventBus;
            _choiceUIService = choiceUIService;
        }

        public void Enter(SinChoicePayload payload)
        {
            _payload = payload;
            Debug.Log($"Entered ChoiceState. offer sin {payload.Sin?.Name}");
            
            _eventBus.RaiseEvent(new SinChoiceOfferedEvent(_payload.Sin, payload.Context));
            _choiceUIService.Show(_payload.Sin, OnAccept, OnRefuse);
        }
        private void OnAccept()
        {
            Debug.Log("Player accepted the sin.");

            _eventBus.RaiseEvent(new SinChoiceMadeEvent(
                _payload.RoomId,
                _payload.LevelIndex,
                _payload.Sin,
                accepted: true,
                _payload.Context
                ));
            //sinresolved next level
        }

        private void OnRefuse()
        {
            Debug.Log("Player refused the sin.");

            _eventBus.RaiseEvent(new SinChoiceMadeEvent(
                _payload.RoomId,
                _payload.LevelIndex,
                _payload.Sin,
                accepted: false,
                _payload.Context
            ));            //boss fight started event bossfightstate
        }

        public void Exit()
        {
            Debug.Log("Exiting ChoiceState.");
            _choiceUIService.Hide();
        }
    }
}
