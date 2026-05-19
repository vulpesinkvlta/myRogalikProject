using Core;
using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace GamePlay
{
    public class LevelFlowController : MonoBehaviour
    {
        [SerializeField] private LevelConfig _levelConfig;

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
            ValidateLevelConfig();
            ValidateRoomTypes();
        }

        private void OnEnable()
        {
            _eventBus.Subscribe<RoomTransitionRequestedEvent>(OnRoomTransitionRequested);
            _eventBus.Subscribe<RoomClearedEvent>(OnRoomCleared);
        }

        private void OnDisable()
        {
            _eventBus.Unsubscribe<RoomTransitionRequestedEvent>(OnRoomTransitionRequested);
            _eventBus.Unsubscribe<RoomClearedEvent>(OnRoomCleared);
        }

        private void Start()
        {
            EnterRoom(_startRoomId);
        }

        private void RegisterRooms()
        {
            _roomsById.Clear();

            foreach (RoomController room in _rooms)
            {
                if(room == null) 
                    continue;
                if(_roomsById.ContainsKey(room.RoomId))
                {
                    Debug.LogError($"Duplicate room id {room.RoomId} found in level flow controller");
                    continue;
                }

                _roomsById.Add(room.RoomId, room);
            }
        }

        private void ValidateLevelConfig()
        {
            if(_levelConfig == null)
            {
               Debug.LogError("Level config is not assigned in level flow controller");
               return;
            }

            Debug.Log(
                $"Level {_levelConfig.LevelIndex} loaded"+
                $"Expected rooms: {_levelConfig.RoomCount}"+
                $"Registred room: {_roomsById.Count}"+
                $"Boss: {_levelConfig.Boss?.name}");

            if(_roomsById.Count != _levelConfig.RoomCount)
            {
                Debug.LogWarning(
                    $"LevelConfig.RoomCount = {_levelConfig.RoomCount}, " +
                    $"but registered rooms count = {_roomsById.Count}",
                    this);
            }

            if(_levelConfig.Boss == null)
            {
                Debug.LogWarning($"LevelConfig.Boss is not assigned", this);
            }
        }

        private void ValidateRoomTypes()
        {
            int startRooms = 0;
            int itemRooms = 0;
            int bossRooms = 0;

            foreach (RoomController room in _roomsById.Values)
            {
                switch (room.RoomType)
                {
                    case RoomType.Start:
                        startRooms++;
                        break;

                    case RoomType.Item:
                        itemRooms++;
                        break;

                    case RoomType.Boss:
                        bossRooms++;
                        break;
                }
            }

            if (startRooms != 1)
                Debug.LogWarning($"Level should have exactly 1 Start room, but has {startRooms}", this);

            if (bossRooms != 1)
                Debug.LogWarning($"Level should have exactly 1 Boss room, but has {bossRooms}", this);

            if (itemRooms > 1)
                Debug.LogWarning($"Level currently should have no more than 1 Item room, but has {itemRooms}", this);
        }

        private void OnRoomTransitionRequested(RoomTransitionRequestedEvent eventData)
        {
            if(_currentRoom == null)
            {
                Debug.LogWarning(
                    $"Ignored transition from room {eventData.FromRoomId}, " +
                    $"because current room is {_currentRoom.RoomId}");

                return;
            }

            if(eventData.FromRoomId != _currentRoom.RoomId)
            {
                Debug.LogWarning($"Room transition requested from room {eventData.FromRoomId}, but current room is {_currentRoom.RoomId}");
                return;
            }
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

        private void OnRoomCleared(RoomClearedEvent eventData)
        {
            Debug.Log($"LevelFlowController: room {eventData.RoomId} cleared. Type: {eventData.RoomType}");

            if (eventData.RoomType == RoomType.Boss)
            {
                Debug.Log("Boss room cleared. Later this will trigger sin resolution / next level.");
            }
        }
    }
}
