namespace Core
{
    public struct RoomClearedEvent
    {
        public readonly int RoomId;
        public readonly RoomType RoomType;
        public RoomClearedEvent(int roomId, RoomType roomType)
        {
            RoomId = roomId;
            RoomType = roomType;
        }
    }

    public enum RoomType
    {
        Start,
        Combat,
        Item,
        Boss,
    }
}
