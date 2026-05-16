using Core;
using UnityEngine;

namespace GamePlay
{
    public class RoomTrigger : MonoBehaviour
    {
        [SerializeField] private RoomController _room;

        private bool _wasTriggered;
    
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (_wasTriggered)
                return;

            if (collision.CompareTag("Player"))
            {
                _wasTriggered = true;
                _room.ActivateRoom();
            }
        }
    }
}
