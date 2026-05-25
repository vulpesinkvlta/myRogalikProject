using Core;
using UnityEngine;

namespace GamePlay
{
    public class RoomEntryPoint : MonoBehaviour
    {
        [SerializeField] private DoorDirection _direction;

        public DoorDirection Direction => _direction;
        public Vector2 Position => transform.position;

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.cyan;
            Gizmos.DrawSphere(transform.position, 0.2f);
        }
    }
}
