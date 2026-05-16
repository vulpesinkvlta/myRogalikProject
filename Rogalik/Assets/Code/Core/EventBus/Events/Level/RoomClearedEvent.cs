namespace Core
{
    public struct RoomClearedEvent
    {
        public readonly int RoomId;

        public RoomClearedEvent(int roomId)
        {
            RoomId = roomId;
        }
    }
}
