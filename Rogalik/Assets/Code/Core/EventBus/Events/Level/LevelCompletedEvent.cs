namespace Core
{
    public struct LevelCompletedEvent
    {
        //public LevelConfig LevelConfig;
        //public SinsConfig Sin;

        public readonly int LevelIndex;
        public readonly int BossRoomId;

        public LevelCompletedEvent(int levelIndex, int bossRoomId)
        {
            LevelIndex = levelIndex;
            BossRoomId = bossRoomId;
        }
    }
}
