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
            _eventBus.Subscribe<BossFightStartedEvent>(OnBossFightStarted);
            _eventBus.Subscribe<SinResolvedEvent>(OnSinResolved);
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
        private void OnBossFightStarted(BossFightStartedEvent eventData)
        {
            Debug.Log("LevelLoopState received BossFightStartedEvent");

            _gameStateMachine.Enter<BossFightState, BossFightStartedEvent>(eventData);
        }

        private void OnSinResolved(SinResolvedEvent eventData)
        {
            Debug.Log($"Sin resolved: {eventData.Sin?.Name}, result: {eventData.Result}");

            // LevelCompletedEvent вызовет LevelFlowController.
            // А LevelLoopState уже поймает LevelCompletedEvent и перейдёт в LevelCompleteState.
        }

        public void Exit()
        {
            Debug.Log("Exited LevelLoopState");
            _eventBus.Unsubscribe<PlayerDiedEvent>(OnPlayerDied);
            _eventBus.Unsubscribe<LevelCompletedEvent>(OnLevelCompleted);
            _eventBus.Unsubscribe<BossEncounterStartedEvent>(OnBossEncounterStarted);
            _eventBus.Unsubscribe<BossFightStartedEvent>(OnBossFightStarted);
            _eventBus.Unsubscribe<SinResolvedEvent>(OnSinResolved);
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
