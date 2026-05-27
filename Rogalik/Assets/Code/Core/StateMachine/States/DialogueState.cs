using GamePlay;
using System;
using UnityEngine;

namespace Core
{
    public class DialogueState : IPayloadedState<BossDialoguePayload>
    {
        private readonly IGameStateMachine _gameStateMachine;
        private readonly IEventBus _eventBus;
        private readonly IDialogueView _dialogueView;

        private BossDialoguePayload _payload;

        public DialogueState(IGameStateMachine gameStateMachine, IEventBus eventBus, IDialogueView dialogueView)
        {
            _gameStateMachine = gameStateMachine;
            _eventBus = eventBus;
            _dialogueView = dialogueView;
        }

        public void Enter(BossDialoguePayload payload)
        {
            _payload = payload;

            Debug.Log($"Entering DialogueState. Dialogue: {_payload.Dialogue}");
    
            _eventBus.RaiseEvent(new DialogueStartedEvent(_payload.Dialogue));
            _dialogueView.Show(_payload.Dialogue, OnDialogueCompleted);
        }

        private void OnDialogueCompleted()
        {
            Debug.Log("Dialogue completed");

            _eventBus.RaiseEvent(new DialogueEndedEvent(_payload.Dialogue));

            SinChoicePayload choicePayload = new SinChoicePayload(
                _payload.RoomId,
                _payload.LevelIndex,
                _payload.Boss,
                _payload.Sin,
                SinOfferContext.BossOffer
            );

            _gameStateMachine.Enter<ChoiceState, SinChoicePayload>(choicePayload);
        }

        public void Exit()
        {
            Debug.Log("Exiting DialogueState.");
            _dialogueView.Hide();
        }
    }
}
