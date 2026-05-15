using Core;
using UnityEngine;

namespace GamePlay
{
    public class RoomTrigger : MonoBehaviour
    {
        [SerializeField] private RoomController _room;


        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
            {
                _room.ActivateRoom();
            }
        }
    }
}
