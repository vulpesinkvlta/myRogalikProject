namespace Core
{
    public struct BossEncounterStartedEvent
    {
        public readonly int RoomId;
        public readonly int LevelIndex;
        public readonly BossConfig Boss;
        public readonly SinsConfig Sin;

        public BossEncounterStartedEvent(
            int roomId,
            int levelIndex,
            BossConfig boss,
            SinsConfig sin)
        {
            RoomId = roomId;
            LevelIndex = levelIndex;
            Boss = boss;
            Sin = sin;
        }
    }
}
