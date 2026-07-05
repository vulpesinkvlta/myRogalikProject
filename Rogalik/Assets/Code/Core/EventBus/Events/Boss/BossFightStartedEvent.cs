namespace Core
{
    public readonly struct BossFightStartedEvent
    {
        public readonly BossConfig Boss;
        public readonly SinsConfig Sin;
        public readonly int RoomId;

        public BossFightStartedEvent(BossConfig boss, SinsConfig sin, int roomId)
        {
            Boss = boss;
            Sin = sin;
            RoomId = roomId;
        }
    }
}