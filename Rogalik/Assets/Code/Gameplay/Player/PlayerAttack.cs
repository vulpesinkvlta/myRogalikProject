using System;
using UnityEngine;
using UnityEngine.InputSystem.iOS;
using Zenject;

namespace Core
{
    public class PlayerAttack : MonoBehaviour
    {
        [Header("Attack Settings")]
        [SerializeField] private LayerMask _damageablelayer;
        [SerializeField] private float _attackThickness = 0.8f;

        private IInputService _inputService;
        private PlayerData _playerData;

        private Vector2 _lastAttackDirection = Vector2.right;

        private float _attackCooldownTimer;

        [Inject]
        public void Construct(IInputService inputService, PlayerData playerData)
        {
            _inputService = inputService;
            _playerData = playerData;
        }

        private void Update()
        {
            UpdateAttackDirection();
            UpdateCooldown();

            if (CanAttack())
                Attack();
        }

        private void UpdateAttackDirection()
        {
            if(_inputService.AttackDirection != Vector2.zero)
                _lastAttackDirection = _inputService.AttackDirection.normalized;
        }
        private void UpdateCooldown()
        {
            if(_attackCooldownTimer > 0)
                _attackCooldownTimer -= Time.deltaTime;
        }

        private bool CanAttack()
        {
            return _inputService.HasAttackInput && _attackCooldownTimer <= 0;
        }

        private void Attack()
        {
            float damage = _playerData.Stats.GetStat(StatType.Damage);
            float attackSpeed = _playerData.Stats.GetStat(StatType.AttackSpeed);

            _attackCooldownTimer = 1f / attackSpeed;

            Vector2 attackSize = GetAttackSize();
            Vector2 attackCenter = GetAttackCenter();
            float attackAngle = GetAttackAngle();

            Collider2D[] hitEnemies = Physics2D.OverlapBoxAll(
                attackCenter,
                attackSize,
                attackAngle, 
                _damageablelayer);

            foreach (Collider2D enemy in hitEnemies)
            {
                if (enemy.TryGetComponent(out IDamageable damageable))
                {
                    damageable.TakeDamage(damage);
                }
            }

            Debug.Log($"Attacked with damage: {damage}, hit {hitEnemies.Length} enemies.");
        }

        private Vector2 GetAttackSize()
        {
            float range = _playerData.Stats.GetStat(StatType.AttackRange);
            return new Vector2(range, _attackThickness);
        }

        private Vector2 GetAttackCenter()
        {
            float range = _playerData.Stats.GetStat(StatType.AttackRange);
            float offset = range * 0.5f;

            return (Vector2)transform.position + _lastAttackDirection * offset;
        }

        private float GetAttackAngle()
        {
            if(Math.Abs(_lastAttackDirection.x) > Math.Abs(_lastAttackDirection.y))
            {
                return 0f;
            }
            return 90f;
        }

        private void OnDrawGizmosSelected()
        {
            if (_playerData == null)
                return;

            Vector2 attackSize = GetAttackSize();
            Vector2 attackCenter = GetAttackCenter();
            float attackAngle = GetAttackAngle();

            Gizmos.color = Color.red;

            Gizmos.matrix = Matrix4x4.TRS(
                attackCenter,
                Quaternion.Euler(0, 0, attackAngle),
                Vector3.one
            );

            Gizmos.DrawWireCube(Vector3.zero, attackSize);
        }
    }
}
