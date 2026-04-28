using UnityEngine;
using Zenject.SpaceFighter;

namespace Core
{
    public class EnemyBrain : MonoBehaviour
    {
        [SerializeField] private EnemiesConfig _enemiesConfig;
        [SerializeField] private Transform _player;

        private EnemyMovement _enemyMovement;
        private EnemyAttack _enemyAttack;
        private EnemyState _state  = EnemyState.Inactive;

        private void Start()
        {
            _enemyMovement = GetComponent<EnemyMovement>();
            _enemyAttack = GetComponent<EnemyAttack>();
        }

        private void Update()
        {
            if(_state == EnemyState.Dead || _state == EnemyState.Inactive) return;

            float distanceToPlayer = Vector2.Distance(transform.position, _player.position);

            if(distanceToPlayer <= _enemiesConfig.AttackRange)
            {
                ChangeState(EnemyState.Attack);
                _enemyMovement.Stop();
                _enemyAttack.TryAttack();

            }

      
            if(distanceToPlayer < _enemiesConfig.DetectionRange)
            {
                ChangeState(EnemyState.Chase);
                _enemyMovement.MoveTo(_player.position);
                return;
            }
            
            ChangeState(EnemyState.Wander);
            _enemyMovement.Wander();
        }
        [ContextMenu("Activate")]
        public void Activate()
        {
           if(_state == EnemyState.Dead) return;
        
           ChangeState(EnemyState.Wander);  
        }

        public void Die()
        {
            ChangeState(EnemyState.Dead);
            _enemyMovement.Stop();
        }

        public void ChangeState(EnemyState newState)
        {
            if(_state == newState) return;
            _state = newState;
        }
    }

    public enum  EnemyState
    {
        Inactive,
        Wander,
        Chase,
        Attack, 
        Dead
    }
}
