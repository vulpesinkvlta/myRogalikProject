using System;
using UnityEngine;
using Zenject;

namespace Core
{
    public class EnemyHealth : MonoBehaviour, IDamageable
    {
        [SerializeField] private EnemiesConfig _config;

        private float _currentHealth;
        private IEventBus _eventBus;

        [Inject]
        public void Construct(IEventBus eventBus)
        {
            _eventBus = eventBus;
        }

        private void Awake()
        {
            _currentHealth = _config.Health;
        }
        public void TakeDamage(float damage)
        {
            _currentHealth -= damage;

            if (_currentHealth <= 0)
            {
                Die();
            }
        }

        private void Die()
        {
            _eventBus.RaiseEvent(new EnemyDiedEvent());
            Destroy(gameObject);
        }
    }
}
