using UnityEngine;

namespace Core
{
    public class MeleeEnemyAttack : MonoBehaviour, IEnemyAttack
    {
        [SerializeField] private EnemiesConfig _config;

        private float _attackCooldownTimer;

        private void Update()
        {
            if( _attackCooldownTimer > 0 )
            {
                _attackCooldownTimer -= Time.deltaTime;
            }
        }
        public bool CanAttack(float distanceToTarget)
        {
            return distanceToTarget <= _config.AttackRange && _attackCooldownTimer <= 0;
        }

        public void TryAttack(IDamageable target)
        {
            if(_attackCooldownTimer > 0)
            return;

            target.TakeDamage(_config.AttackDamage);
            _attackCooldownTimer = _config.AttackCooldown;

            Debug.Log($"{name} attacked player by {_config.AttackDamage} damage");
        }
    }
}
