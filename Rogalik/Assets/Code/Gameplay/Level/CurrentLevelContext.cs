namespace GamePlay
{
    public class CurrentLevelContext
    {
        public LevelConfig LevelConfig { get; }

        public int LevelIndex => LevelConfig.LevelIndex;
        public SinsConfig SinsConfig => LevelConfig.Sin;
        public BossConfig BossConfig => LevelConfig.Boss;
        public int RoomCount => LevelConfig.RoomCount;

        public CurrentLevelContext(LevelConfig levelConfig)
        {
            LevelConfig = levelConfig;
        }
    }
}
