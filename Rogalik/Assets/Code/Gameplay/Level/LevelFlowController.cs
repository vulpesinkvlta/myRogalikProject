using Core;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace GamePlay
{
    public class LevelFlowController : MonoBehaviour
    {
        [Header("Level")]
        private CurrentLevelContext _levelContext;

        [Header("Rooms")]
        [SerializeField] private RoomController[] _rooms;
        [SerializeField] private int _startRoomId;

        [Header("Player")]
        [SerializeField] private Rigidbody2D _playerRigidbody;

        [Header("Camera")]
        [SerializeField] private RoomCameraController _roomCameraController;

        private readonly Dictionary<int, RoomController> _roomsById = new();

        private IEventBus _eventBus;
        private RoomController _currentRoom;

        private bool _isTransitionLocked;

        [Inject]
        public void Construct(IEventBus eventBus, CurrentLevelContext currentLevelContext)
        {
            _eventBus = eventBus;
            _levelContext = currentLevelContext;
        }

        private void Awake()
        {
            RegisterRooms();
            ValidateLevelConfig();
            ValidateRoomTypes();
        }

        private void Start()
        {
            _eventBus.Subscribe<RoomTransitionRequestedEvent>(OnRoomTransitionRequested);
            _eventBus.Subscribe<RoomClearedEvent>(OnRoomCleared);

            EnterRoom(_startRoomId);
        }

        private void OnDestroy()
        {
            _eventBus.Unsubscribe<RoomTransitionRequestedEvent>(OnRoomTransitionRequested);
            _eventBus.Unsubscribe<RoomClearedEvent>(OnRoomCleared);
        }

        private void RegisterRooms()
        {
            _roomsById.Clear();

            foreach (RoomController room in _rooms)
            {
                if (room == null)
                    continue;

                if (_roomsById.ContainsKey(room.RoomId))
                {
                    Debug.LogError($"Duplicate RoomId found: {room.RoomId}", room);
                    continue;
                }

                _roomsById.Add(room.RoomId, room);
            }
        }

        private void OnRoomTransitionRequested(RoomTransitionRequestedEvent eventData)
        {
            if (_isTransitionLocked)
                return;

            if (_currentRoom == null)
                return;

            if (eventData.FromRoomId != _currentRoom.RoomId)
                return;

            if (!_roomsById.TryGetValue(eventData.ToRoomId, out RoomController targetRoom))
            {
                Debug.LogError($"Target room {eventData.ToRoomId} not found");
                return;
            }

            DoorDirection entryDirection = eventData.Direction.GetOpposite();
            Vector2 entryPosition = targetRoom.GetEntryPosition(entryDirection);

            Vector3 oldPlayerPosition = _playerRigidbody.position;
            Vector3 newPlayerPosition = entryPosition;
            Vector3 positionDelta = newPlayerPosition - oldPlayerPosition;

            if (_roomCameraController != null)
                _roomCameraController.SetRoom(targetRoom);

            TeleportPlayer(entryPosition);

            if (_roomCameraController != null)
                _roomCameraController.NotifyPlayerWarped(_playerRigidbody.transform, positionDelta);

            EnterRoom(eventData.ToRoomId);

            LockTransitionShortly();
        }

        private void TeleportPlayer(Vector2 position)
        {
            if (_playerRigidbody == null)
            {
                Debug.LogError("Player Rigidbody2D is not assigned", this);
                return;
            }

            _playerRigidbody.linearVelocity = Vector2.zero;
            _playerRigidbody.angularVelocity = 0f;
            _playerRigidbody.position = position;
        }

        private void EnterRoom(int roomId)
        {
            if (!_roomsById.TryGetValue(roomId, out RoomController room))
            {
                Debug.LogError($"Room with id {roomId} not found");
                return;
            }

            _currentRoom = room;

            if(_roomCameraController != null)
                _roomCameraController.SetRoom(_currentRoom);

            if (_currentRoom.RoomType == RoomType.Boss &&
                _currentRoom.State == RoomState.Unvisited)
            {
                StartBossEncounter(_currentRoom);
                return;
            }

            _currentRoom.ActivateRoom();

            Debug.Log($"Entered room: {roomId}");
        }

        private void StartBossEncounter(RoomController currentRoom)
        {
            Debug.Log($"Boss encounter started in room {currentRoom.RoomId}");
            _eventBus.RaiseEvent(new BossEncounterStartedEvent(
                currentRoom.RoomId,
                _levelContext.LevelIndex,
                _levelContext.BossConfig,
                _levelContext.SinsConfig    
            ));
        }

        private void LockTransitionShortly()
        {
            _isTransitionLocked = true;
            StartCoroutine(UnlockTransitionAfterDelay());
        }

        private IEnumerator UnlockTransitionAfterDelay()
        {
            yield return new WaitForSeconds(0.15f);
            _isTransitionLocked = false;
        }

        private void OnRoomCleared(RoomClearedEvent eventData)
        {
            Debug.Log($"Room cleared: {eventData.RoomId}, type: {eventData.RoomType}");

            if (eventData.RoomType == RoomType.Boss)
                CompleteLevel(eventData.RoomId);
        }

        private void CompleteLevel(int bossRoomId)
        {
            if (_levelContext == null)
            {
                Debug.LogError("Cannot complete level: LevelConfig is not assigned");
                return;
            }

            Debug.Log($"Level {_levelContext.LevelIndex} completed");

            _eventBus.RaiseEvent(new LevelCompletedEvent(
                _levelContext.LevelIndex,
                bossRoomId
            ));
        }

        private void ValidateLevelConfig()
        {
            if (_levelContext == null || _levelContext.LevelConfig == null)
            {
                Debug.LogError("CurrentLevelContext or LevelConfig is missing", this);
                return;
            }

            Debug.Log(
                $"Level {_levelContext.LevelIndex} loaded. " +
                $"Sin: {_levelContext.SinsConfig?.Name}. " +
                $"Boss: {_levelContext.BossConfig?.name}. " +
                $"Expected rooms: {_levelContext.LevelConfig.RoomCount}. " +
                $"Registered rooms: {_roomsById.Count}"
            );

            if (_levelContext.LevelConfig.RoomCount != _roomsById.Count)
            {
                Debug.LogWarning(
                    $"LevelConfig.RoomCount = {_levelContext.LevelConfig.RoomCount}, " +
                    $"but registered rooms count = {_roomsById.Count}",
                    this
                );
            }

            if (_levelContext.SinsConfig == null)
            {
                Debug.LogWarning($"Level {_levelContext.LevelIndex} has no Sin assigned", this);
            }

            if (_levelContext.BossConfig == null)
            {
                Debug.LogWarning($"Level {_levelContext.LevelIndex} has no Boss assigned", this);
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
    }
}