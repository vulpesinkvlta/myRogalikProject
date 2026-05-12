using UnityEngine;

namespace Core
{
    public class RangedEnemyAttack : MonoBehaviour, IEnemyAttack
    {
        [SerializeField] private EnemiesConfig _config;
        [SerializeField] private Transform _attackPoint;
        [SerializeField] private Transform _target;
        
        private float _attackCooldown;

        private void Update()
        {
            if(_attackCooldown > 0 )
            {
                _attackCooldown -= Time.deltaTime;
            }
        }
        public bool CanAttack(float distanceToTarget)
        {
            return distanceToTarget <= _config.AttackRange && _attackCooldown <= 0;
        }

        public void TryAttack(IDamageable target)
        {
            if(_attackCooldown > 0)
                return;

            Vector2 direction  = (_target.position - _attackPoint.position).normalized;
            
            GameObject projectileObject = Instantiate(_config.ProjectilePrefab, _attackPoint.position, Quaternion.identity);
            
            EnemyProjectile projectile = projectileObject.GetComponent<EnemyProjectile>();
            projectile.Initialize(direction, _config.AttackDamage, _config.ProjectileSpeed);
            _attackCooldown = _config.AttackCooldown;


            Debug.Log($"{name} ranged attacked");
        }
    }
}
