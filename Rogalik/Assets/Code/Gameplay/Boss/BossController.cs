using Core;
using UnityEngine;

namespace GamePlay
{
    public class BossController : MonoBehaviour
    {
        private BossHealth _health;
        private BossState _state = BossState.Inactive;

        public BossState State => _state;

        private void Awake()
        {
            _health = GetComponent<BossHealth>();
        }

        public void Activate(BossConfig boss, SinsConfig sin, int roomId)
        {
            if (_state == BossState.Dead)
                return;

            _state = BossState.Active;

            _health.Initialize(boss, sin, roomId);

            gameObject.SetActive(true);

            Debug.Log($"Boss activated: {boss?.Name}");
        }

        public void Die()
        {
            if (_state == BossState.Dead)
                return;

            _state = BossState.Dead;

            Debug.Log("Boss state changed to Dead");
        }
    }
}