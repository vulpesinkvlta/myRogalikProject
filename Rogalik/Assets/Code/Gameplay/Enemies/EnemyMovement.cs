using UnityEngine;

namespace Core
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class EnemyMovement : MonoBehaviour
    {
        [SerializeField] private EnemiesConfig _enemiesConfig;

        private Rigidbody2D _rigidbody2D;

        private Vector2 _wanderDirection;
        private float _wanderTimer;

        private void Start()
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();
        }
        public void MoveTo(Vector2 position)
        {
            Vector2 direction = (position - _rigidbody2D.position).normalized;
            Move(direction);
        }

        private void Move(Vector2 direction)
        {
            Vector2 nextPosition = _rigidbody2D.position + direction * _enemiesConfig.Speed * Time.deltaTime;

            _rigidbody2D.MovePosition(nextPosition);
        }

        public void Stop()
        {
            _rigidbody2D.linearVelocity = Vector2.zero;
        }

        public void Wander()
        {
            _wanderTimer -= Time.deltaTime;

            if(_wanderTimer <= 0)
            {
                _wanderDirection = Random.insideUnitCircle.normalized;
                _wanderTimer = _enemiesConfig.WanderChangeDirectionTime;
            }

            Move(_wanderDirection);
        }
    }
}