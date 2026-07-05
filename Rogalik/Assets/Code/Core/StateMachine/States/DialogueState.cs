using GamePlay;
using System;
using UnityEngine;

namespace Core
{
    public class DialogueState : IPayloadedState<BossDialoguePayload>
    {
        private readonly IGameStateMachine _stateMachine;
        private readonly IEventBus _eventBus;
        private readonly IDialogueUIService _dialogueUIService;

        private BossDialoguePayload _payload;

        public DialogueState(
            IGameStateMachine stateMachine,
            IEventBus eventBus,
            IDialogueUIService dialogueUIService)
        {
            _stateMachine = stateMachine;
            _eventBus = eventBus;
            _dialogueUIService = dialogueUIService;
        }

        public void Enter(BossDialoguePayload payload)
        {
            _payload = payload;

            _eventBus.RaiseEvent(new DialogueStartedEvent(_payload.Dialogue));

            _dialogueUIService.Show(_payload.Dialogue, OnDialogueCompleted);
        }

        private void OnDialogueCompleted()
        {
            _eventBus.RaiseEvent(new DialogueEndedEvent(_payload.Dialogue));

            SinChoicePayload choicePayload = new SinChoicePayload(
                _payload.RoomId,
                _payload.LevelIndex,
                _payload.Boss,
                _payload.Sin,
                SinOfferContext.BossOffer
            );

            _stateMachine.Enter<ChoiceState, SinChoicePayload>(choicePayload);
        }

        public void Exit()
        {
            _dialogueUIService.Hide();
        }
    }
}
