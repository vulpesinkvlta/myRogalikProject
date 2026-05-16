using Core;
using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace GamePlay
{
    public class LevelFlowController : MonoBehaviour
    {
        [SerializeField] private RoomController[] _rooms;
        [SerializeField] private int _startRoomId;

        private readonly Dictionary<int, RoomController> _roomsById = new Dictionary<int, RoomController>();
        
        private IEventBus _eventBus;
        private RoomController _currentRoom;

        [Inject]
        public void Construct(IEventBus eventBus)
        {
            _eventBus = eventBus;   
        }

        private void Awake()
        {
            RegisterRooms();
        }

        private void OnEnable()
        {
            _eventBus.Subscribe<RoomTransitionRequestedEvent>(OnRoomTransitionRequested);
        }

        private void OnDisable()
        {
            _eventBus.Unsubscribe<RoomTransitionRequestedEvent>(OnRoomTransitionRequested);
        }

        private void Start()
        {
            EnterRoom(_startRoomId);
        }

        private void RegisterRooms()
        {
            foreach (RoomController room in _rooms)
            {
                if(room == null) 
                    continue;
                _roomsById[room.RoomId] = room;
            }
        }

        private void OnRoomTransitionRequested(RoomTransitionRequestedEvent eventData)
        {
            Debug.Log($"Transition from room {eventData.FromRoomId} to room {eventData.ToRoomId}");

            EnterRoom(eventData.ToRoomId);
        }
        private void EnterRoom(int roomId)
        {
            if(!_roomsById.TryGetValue(roomId, out RoomController room))
            {
                Debug.LogError($"Room with id {roomId} not found");
                return;
            }

            _currentRoom = room;
            _currentRoom.ActivateRoom();

            Debug.Log($"Entered room: {roomId}");
        }
    }
}
