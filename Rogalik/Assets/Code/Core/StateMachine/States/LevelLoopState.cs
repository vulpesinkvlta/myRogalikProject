using System;
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
            _eventBus.Subscribe<LevelCompletedEvent>(OnLevelCompleted);
            _eventBus.Subscribe<BossEncounterStartedEvent>(OnBossEncounterStarted);
        }

        private void OnBossEncounterStarted(BossEncounterStartedEvent eventData)
        {
            Debug.Log(
                $"Boss encounter received. " +
                $"Level: {eventData.LevelIndex}, " +
                $"Boss: {eventData.Boss?.name}, " +
                $"Sin: {eventData.Sin?.Name}"
            );

            DialogueConfig dialogueConfig = eventData.Boss.OfferDialogue;

            BossDialoguePayload payload = new BossDialoguePayload
            (
                eventData.RoomId,
                eventData.LevelIndex,
                eventData.Boss,
                eventData.Sin,
                dialogueConfig
            );

            _gameStateMachine.Enter<DialogueState, BossDialoguePayload>(payload);
        }

        public void Exit()
        {
            Debug.Log("Exited LevelLoopState");
            _eventBus.Unsubscribe<PlayerDiedEvent>(OnPlayerDied);
            _eventBus.Unsubscribe<LevelCompletedEvent>(OnLevelCompleted);
            _eventBus.Unsubscribe<BossEncounterStartedEvent>(OnBossEncounterStarted);
        }
        private void OnLevelCompleted(LevelCompletedEvent @event)
        {
            _gameStateMachine.Enter<LevelCompleteState>();
        }

        private void OnPlayerDied(PlayerDiedEvent evt)
        {
            _gameStateMachine.Enter<PlayerDeathState>();
        }
    }
}
