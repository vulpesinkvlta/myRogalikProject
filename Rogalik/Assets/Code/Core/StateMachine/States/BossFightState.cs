using UnityEngine;

namespace Core
{
    public class BossFightState : IPayloadedState<BossFightStartedEvent>
    {
        public void Enter(BossFightStartedEvent payload)
        {
            Debug.Log(
                $"Entered BossFightState. " +
                $"Boss: {payload.Boss?.name}, " +
                $"Sin: {payload.Sin?.Name}, " +
                $"Room: {payload.RoomId}"
            );

            // Позже:
            // - активировать босса
            // - закрыть двери boss room
            // - включить boss AI
        }

        public void Exit()
        {
            Debug.Log("Exited BossFightState");
        }
    }
}