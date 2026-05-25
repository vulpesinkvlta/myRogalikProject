using GamePlay;
using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Core
{
    public class RoomController : MonoBehaviour
    {
        [Header("Room Data")]
        [SerializeField] private int _roomId;
        [SerializeField] private RoomType _roomType;

        [Header("Room Content")]
        [SerializeField] private EnemyBrain[] _enemies;
        [SerializeField] private RoomDoor[] _doors;
        [SerializeField] private RoomEntryPoint[] _entryPoints;

        [Header("Camera")]
        [SerializeField] private Collider2D _cameraBounds;

        private readonly Dictionary<DoorDirection, RoomEntryPoint> _entryPointsByDirection = new();

        private IEventBus _eventBus;

        private RoomState _state = RoomState.Unvisited;
        private int _aliveEnemies;

        public int RoomId => _roomId;
        public RoomType RoomType => _roomType;
        public RoomState State => _state;
        public Collider2D CameraBounds => _cameraBounds;

        public bool IsCleared => _state == RoomState.Cleared;

        [Inject]
        public void Construct(IEventBus eventBus)
        {
            _eventBus = eventBus;
        }

        private void Awake()
        {
            RegisterEntryPoints();
            InitializeDoors();
            SubscribeToEnemies();

            OpenDoors();
        }

        private void OnDestroy()
        {
            UnsubscribeFromEnemies();
        }

        public void ActivateRoom()
        {
            if (_state == RoomState.Cleared)
            {
                OpenDoors();
                Debug.Log($"Room {_roomId} already cleared");
                return;
            }

            if (_state == RoomState.Active)
                return;

            _state = RoomState.Active;

            Debug.Log($"Room {_roomId} activated. Type: {_roomType}. Enemies: {_aliveEnemies}");

            if (_aliveEnemies == 0)
            {
                ClearRoom();
                return;
            }

            CloseDoors();
            ActivateEnemies();
        }

        public Vector2 GetEntryPosition(DoorDirection entryDirection)
        {
            if (_entryPointsByDirection.TryGetValue(entryDirection, out RoomEntryPoint entryPoint))
                return entryPoint.Position;

            Debug.LogWarning($"Room {_roomId} has no entry point for direction {entryDirection}", this);
            return transform.position;
        }

        private void ActivateEnemies()
        {
            foreach (EnemyBrain enemy in _enemies)
            {
                if (enemy != null)
                    enemy.Activate();
            }
        }

        private void RegisterEntryPoints()
        {
            _entryPointsByDirection.Clear();

            foreach (RoomEntryPoint entryPoint in _entryPoints)
            {
                if (entryPoint == null)
                    continue;

                if (_entryPointsByDirection.ContainsKey(entryPoint.Direction))
                {
                    Debug.LogError(
                        $"Room {_roomId} has duplicate entry point for {entryPoint.Direction}",
                        entryPoint
                    );

                    continue;
                }

                _entryPointsByDirection.Add(entryPoint.Direction, entryPoint);
            }
        }

        private void InitializeDoors()
        {
            foreach (RoomDoor door in _doors)
            {
                if (door != null)
                    door.Initialize(_roomId);
            }
        }

        private void SubscribeToEnemies()
        {
            _aliveEnemies = 0;

            foreach (EnemyBrain enemy in _enemies)
            {
                if (enemy == null)
                    continue;

                EnemyHealth health = enemy.GetComponent<EnemyHealth>();

                if (health == null)
                    continue;

                health.OnDeath += OnEnemyDied;
                _aliveEnemies++;
            }
        }

        private void UnsubscribeFromEnemies()
        {
            foreach (EnemyBrain enemy in _enemies)
            {
                if (enemy == null)
                    continue;

                EnemyHealth health = enemy.GetComponent<EnemyHealth>();

                if (health != null)
                    health.OnDeath -= OnEnemyDied;
            }
        }

        private void OnEnemyDied(EnemyHealth enemyHealth)
        {
            if (_state == RoomState.Cleared)
                return;

            _aliveEnemies--;

            Debug.Log($"Room {_roomId}: enemy died. Alive: {_aliveEnemies}");

            if (_aliveEnemies <= 0)
                ClearRoom();
        }

        private void ClearRoom()
        {
            if (_state == RoomState.Cleared)
                return;

            _state = RoomState.Cleared;

            OpenDoors();

            Debug.Log($"Room {_roomId} cleared. Type: {_roomType}");

            _eventBus.RaiseEvent(new RoomClearedEvent(_roomId, _roomType));
        }

        private void OpenDoors()
        {
            foreach (RoomDoor door in _doors)
            {
                if (door != null)
                    door.Open();
            }
        }

        private void CloseDoors()
        {
            foreach (RoomDoor door in _doors)
            {
                if (door != null)
                    door.Close();
            }
        }
    }

    public enum RoomState
    {
        Unvisited,
        Active,
        Cleared
    }
}
