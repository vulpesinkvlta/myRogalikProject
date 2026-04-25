using System;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

namespace Core
{
    public class KeyboardInputService : IInputService, IInitializable, ITickable, IDisposable
    {
        private GameInput _gameInput;
        public Vector2 MoveDirection { get; private set; }
        public Vector2 AttackDirection { get; private set; }
        public bool HasAttackInput => AttackDirection != Vector2.zero;

        Vector2 IInputService.MoveDirection => MoveDirection;

        Vector2 IInputService.AttackDirection => AttackDirection;

        public void Initialize()
        {
            _gameInput = new GameInput();
            _gameInput.Enable();
        }

        public void Tick()
        {
            MoveDirection = _gameInput.Gameplay.Movement.ReadValue<Vector2>();
            AttackDirection = _gameInput.Gameplay.Attack.ReadValue<Vector2>();
        }


        void IDisposable.Dispose()
        {
            _gameInput.Disable();
            _gameInput.Dispose();
        }
    }
}
