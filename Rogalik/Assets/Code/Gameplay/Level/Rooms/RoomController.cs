using GamePlay;
using System;
using UnityEngine;
using Zenject;

namespace Core
{
    public class RoomController : MonoBehaviour
    {
        [SerializeField] private int _roomId;
        [SerializeField] private EnemyBrain[] _enemies;
        [SerializeField] private RoomDoor[] _doors;

        private IEventBus _eventBus;

        private bool _isActivated;
        private bool _isCleared;
        private int _aliveEnemies;

        public int RoomId => _roomId;

        [Inject]
        public void Construct(IEventBus eventBus)
        {
            _eventBus = eventBus;
        }

        private void Awake()
        {
            _aliveEnemies = _enemies.Length;

            InitializeDoors();
            SubscribeToEnemies();

            OpenDoors();
        }


        private void SubscribeToEnemies()
        {
            foreach (EnemyBrain enemy in _enemies)
            {
                if (enemy == null)
                    continue;

                EnemyHealth health = enemy.GetComponent<EnemyHealth>();

                if (health != null)
                    health.OnDeath += OnEnemyDied;
            }
        }

        private void OnDestroy()
        {
            UnsubscribeFromEnemies();
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

        public void ActivateRoom()
        {
            if (_isActivated || _isCleared)
                return;

            _isActivated = true;
            //_aliveEnemies = _enemies.Length;
            CloseDoors();

            if (_aliveEnemies == 0)
            {
                ClearRoom();
                return;
            }

            foreach (var enemy in _enemies)
            {
                enemy.Activate();
            }

            Debug.Log("Room activated");
        }

        private void InitializeDoors()
        {
            foreach (RoomDoor door in _doors)
            {
                door.Initialize(_roomId);
            }
        }

        public void OnEnemyDied(EnemyHealth enemyHealth)
        {
            if (_isCleared)
                return;

            _aliveEnemies--;

            if (_aliveEnemies <= 0)
                ClearRoom();
        }

        private void ClearRoom()
        {
            if (_isCleared)
                return;

            _isCleared = true;

            Debug.Log("Room cleared");

            _eventBus.RaiseEvent(new RoomClearedEvent(_roomId));
            // позже:
            // открыть двери
            // выдать награду
        }
        private void OpenDoors()
        {
            foreach (RoomDoor door in _doors)
            {
                door.Open();
            }
        }

        private void CloseDoors()
        {
            foreach (RoomDoor door in _doors)
            {
                door.Close();
            }
        }
    }
}
