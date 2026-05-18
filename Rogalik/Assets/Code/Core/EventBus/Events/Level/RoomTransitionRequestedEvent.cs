namespace Core
{
    public struct RoomTransitionRequestedEvent
    {
        public readonly int FromRoomId;
        public readonly int ToRoomId;
        public readonly DoorDirection Direction;

        public RoomTransitionRequestedEvent(int fromRoomId,
            int toRoomId, DoorDirection direction)
        {
            FromRoomId = fromRoomId;
            ToRoomId = toRoomId;    
            Direction = direction;
        }
    }

    public enum DoorDirection
    {
        Up,
        Down,
        Left,
        Right
    }
}
