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
        private EnemyBrain _enemyBrain;
        private bool _isDead;

        public event Action<EnemyHealth> OnDeath;

        [Inject]
        public void Construct(IEventBus eventBus)
        {
            _eventBus = eventBus;
        }

        private void Awake()
        {
            _enemyBrain = GetComponent<EnemyBrain>();
            _currentHealth = _config.Health;
        }

        public void TakeDamage(float damage)
        {
            if (_isDead) 
                return; 

            _currentHealth -= damage;

            if (_currentHealth <= 0)
            {
                _currentHealth = 0;
                Die();
            }
        }

        private void Die()
        {
            if (_isDead)
                return;

            _isDead = true;

            _enemyBrain.Die();
            OnDeath?.Invoke(this);
            _eventBus.RaiseEvent(new EnemyDiedEvent());
            Destroy(gameObject);
        }
    }
}
