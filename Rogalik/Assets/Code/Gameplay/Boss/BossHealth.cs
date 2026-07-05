using Core;
using UnityEngine;
using Zenject;

namespace GamePlay
{
    public class BossHealth : MonoBehaviour, IDamageable
    {
        private IEventBus _eventBus;
        private BossController _bossController;

        private BossConfig _boss;
        private SinsConfig _sin;
        private int _roomId;

        private float _currentHealth;
        private bool _isInitialized;
        private bool _isDead;

        [Inject]
        public void Construct(IEventBus eventBus)
        {
            _eventBus = eventBus;
        }

        private void Awake()
        {
            _bossController = GetComponent<BossController>();
        }

        public void Initialize(BossConfig boss, SinsConfig sin, int roomId)
        {
            _boss = boss;
            _sin = sin;
            _roomId = roomId;

            _currentHealth = boss.Health;

            _isInitialized = true;
            _isDead = false;

            Debug.Log($"Boss health initialized: {_currentHealth}");
        }

        public void TakeDamage(float damage)
        {
            if (!_isInitialized || _isDead)
                return;

            _currentHealth -= damage;

            Debug.Log($"Boss took damage: {damage}. HP: {_currentHealth}");

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

            _bossController.Die();

            _eventBus.RaiseEvent(new BossDefeatedEvent(
                _boss,
                _sin,
                _roomId
            ));

            Destroy(gameObject);
        }
    }
}