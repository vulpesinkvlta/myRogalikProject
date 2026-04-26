using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace Core
{
    public class PlayerHealth : MonoBehaviour, IDamageable
    {
        private PlayerData _playerData;
        private IEventBus _eventBus;
        private bool _isDead;

        [Inject]
        public void Construct(PlayerData data, IEventBus eventBus)
        {
            _playerData = data;
            _eventBus = eventBus;
        }
        public void TakeDamage(float damage)
        {
            if (_isDead)
                return;
            _playerData.CurrentHealth -= damage;

            if (_playerData.CurrentHealth <= 0)
            {
                _playerData.CurrentHealth = 0;
                _isDead = true;
                _eventBus.RaiseEvent(new PlayerDiedEvent());
            }
        }
    }
}
