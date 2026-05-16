using Core;
using UnityEngine;
using Zenject;

namespace GamePlay
{
    public class RoomDoor : MonoBehaviour
    {
        [Header("Door Data")]
        [SerializeField] private DoorDirection _direction;
        [SerializeField] private int _targetRoomId = -1;

        [Header("Door Parts")]
        [SerializeField] private Collider2D _blocker;
        [SerializeField] private Collider2D _transitionTrigger;

        [Header("View")]
        [SerializeField] private GameObject _closedView;
        [SerializeField] private GameObject _openedView;

        private IEventBus _eventBus;

        private int _ownerRoomId;
        private bool _isOpen;

        public DoorDirection Direction => _direction;
        public int TargetRoomId => _targetRoomId;

        [Inject]
        public void Construct(IEventBus eventBus)
        {
            _eventBus = eventBus;
        }

        public void Initialize(int ownerRoomId)
        {
            _ownerRoomId = ownerRoomId;
        }

        public void Open()
        {
            _isOpen = true;

            if (_blocker != null)
                _blocker.enabled = false;

            if (_transitionTrigger != null)
                _transitionTrigger.enabled = true;

            if (_closedView != null)
                _closedView.SetActive(false);

            if (_openedView != null)
                _openedView.SetActive(true);
        }

        public void Close()
        {
            _isOpen = false;

            if (_blocker != null)
                _blocker.enabled = true;

            if (_transitionTrigger != null)
                _transitionTrigger.enabled = false;

            if (_closedView != null)
                _closedView.SetActive(true);

            if (_openedView != null)
                _openedView.SetActive(false);
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (!_isOpen)
                return;

            if (_targetRoomId < 0)
            {
                Debug.LogWarning($"{name}: TargetRoomId is not assigned");
                return;
            }

            if (collision.GetComponentInParent<PlayerHealth>() == null)
                return;

            _eventBus.RaiseEvent(new RoomTransitionRequestedEvent(
                _ownerRoomId,
                _targetRoomId,
                _direction
            ));
        }
    }
}
