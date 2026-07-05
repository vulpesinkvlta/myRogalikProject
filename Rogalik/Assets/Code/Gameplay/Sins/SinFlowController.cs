using GamePlay;
using System;
using UnityEngine;
using Zenject;

namespace Core
{
    public class SinFlowController : IInitializable, IDisposable
    {
        private readonly IEventBus _eventBus;
        private readonly CurrentLevelContext _levelContext;

        public SinFlowController(
            IEventBus eventBus,
            CurrentLevelContext levelContext)
        {
            _eventBus = eventBus;
            _levelContext = levelContext;
        }

        public void Initialize()
        {
            _eventBus.Subscribe<SinChoiceMadeEvent>(OnSinChoiceMade);
            _eventBus.Subscribe<BossDefeatedEvent>(OnBossDefeated);
        }

        public void Dispose()
        {
            _eventBus.Unsubscribe<SinChoiceMadeEvent>(OnSinChoiceMade);
            _eventBus.Unsubscribe<BossDefeatedEvent>(OnBossDefeated);
        }

        private void OnSinChoiceMade(SinChoiceMadeEvent eventData)
        {
            if (eventData.Context != SinOfferContext.BossOffer)
                return;

            if (eventData.Accepted)
            {
                ResolveSinAsAccepted(eventData);
                return;
            }

            StartBossFight(eventData);
        }

        private void ResolveSinAsAccepted(SinChoiceMadeEvent eventData)
        {
            Debug.Log($"Sin accepted: {eventData.Sin?.Name}");

            _eventBus.RaiseEvent(new SinResolvedEvent(
                eventData.Sin,
                SinResolutionType.Accepted
            ));
        }

        private void StartBossFight(SinChoiceMadeEvent eventData)
        {
            Debug.Log($"Sin refused. Boss fight started. Boss: {_levelContext.BossConfig?.name}");

            _eventBus.RaiseEvent(new BossFightStartedEvent(
                _levelContext.BossConfig,
                eventData.Sin,
                eventData.RoomId
            ));
        }

        private void OnBossDefeated(BossDefeatedEvent eventData)
        {
            Debug.Log($"Boss defeated: {eventData.Boss?.Name}. Sin purified: {eventData.Sin?.Name}");

            _eventBus.RaiseEvent(new SinResolvedEvent(
                eventData.Sin,
                SinResolutionType.Purified
            ));
        }
    }
}