using System;
using UnityEngine;
using Zenject;

namespace Core
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class PlayerMovement : MonoBehaviour
    {
        private Rigidbody2D _rigidbody2D;
        private PlayerData _playerData;
        private IInputService _inputService;

        [Inject]
        public void Construct(PlayerData playerData, IInputService inputService)
        {
            _playerData = playerData;
            _inputService = inputService;
        }

        private void Awake()
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();
        }

        private void FixedUpdate()
        {
            Move();
        }

        private void Move()
        {
            Vector2 direction = _inputService.MoveDirection.normalized;
            float speed = _playerData.Stats.GetStat(StatType.MoveSpeed);
            Vector2 nextPostion = _rigidbody2D.position + direction * speed * Time.fixedDeltaTime;  
            _rigidbody2D.MovePosition(nextPostion); 
        }
    }
}
