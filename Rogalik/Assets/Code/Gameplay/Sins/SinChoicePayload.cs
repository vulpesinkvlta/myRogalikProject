namespace Core
{
    public readonly struct SinChoicePayload
    {
        public readonly int RoomId;
        public readonly int LevelIndex;
        public readonly BossConfig Boss;
        public readonly SinsConfig Sin;
        public readonly SinOfferContext Context;

        public SinChoicePayload(int roomId, int levelIndex, BossConfig boss, SinsConfig sin, SinOfferContext context)
        {
            RoomId = roomId;
            LevelIndex = levelIndex;
            Boss = boss;
            Sin = sin;
            Context = context;
        }
    }
}
