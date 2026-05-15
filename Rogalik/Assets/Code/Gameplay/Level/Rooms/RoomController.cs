using System;
using UnityEngine;

namespace Core
{
    public class RoomController : MonoBehaviour
    {
        [SerializeField] private EnemyBrain[] _enemies;
        
        private bool _isActivated;
        private int _aliveEnemies;

        public void ActivateRoom()
        {
           if (_isActivated)
                return;
            _isActivated = true;
            _aliveEnemies = _enemies.Length;
            foreach (var enemy in _enemies)
            {
               enemy.Activate();
            }

            Debug.Log("Room activated");
        }

        public void OnEnemyDied()
        {
            _aliveEnemies--;
            
            if(_aliveEnemies <= 0)
            {
                ClearRoom();
            }
        }

        private void ClearRoom()
        {
            Debug.Log("Room cleared");
            // позже:
            // открыть двери
            // выдать награду
            // RaiseEvent(new RoomClearedEvent())
        }
    }
}
