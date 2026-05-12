using System;
using UnityEngine;

namespace Core
{
    public class EnemyProjectile : MonoBehaviour
    {
        private Vector2 _direction;
        private float _damage;
        private float _speed;
        public void Initialize(Vector2 direction, float attackDamage, float projectileSpeed)
        {
            _direction = direction;
            _damage = attackDamage;
            _speed = projectileSpeed;
        }

        private void Update()
        {
            transform.position += (Vector3)(_direction * _speed * Time.deltaTime);
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if(collision.TryGetComponent(out IDamageable damageable))
            {
                damageable.TakeDamage(_damage);

                Destroy(gameObject);
            }
        }
    }
}