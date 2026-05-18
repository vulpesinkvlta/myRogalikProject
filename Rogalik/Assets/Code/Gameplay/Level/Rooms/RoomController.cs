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
        [SerializeField] private RoomType _roomType;
        [SerializeField] private RoomRewardSpawner _roomRewardSpawner;

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
            InitializeDoors();
            SubscribeToEnemies();

            OpenDoors();
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

            CloseDoors();

            if (_aliveEnemies == 0)
            {
                ClearRoom();
                return;
            }

            foreach (var enemy in _enemies)
            {
                if (enemy != null)
                    enemy.Activate();
            }

            Debug.Log($"Room {_roomId} activated. Room type: {_roomType}");
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
            {
                ClearRoom();
            }
        }

        private void ClearRoom()
        {
            if (_isCleared)
                return;

            _isCleared = true;

            OpenDoors();

            Debug.Log("Room cleared");


            if (_roomRewardSpawner != null)
            {
                _roomRewardSpawner.TrySpawnReward();
            }

            _eventBus.RaiseEvent(new RoomClearedEvent(_roomId, _roomType));
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
