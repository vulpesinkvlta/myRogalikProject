namespace Core
{
    public readonly struct SinChoiceMadeEvent
    {
        public readonly int RoomId;
        public readonly int LevelIndex;
        public readonly SinsConfig Sin;
        public readonly bool Accepted;
        public readonly SinOfferContext Context;

        public SinChoiceMadeEvent(
            int roomId,
            int levelIndex,
            SinsConfig sin,
            bool accepted,
            SinOfferContext context)
        {
            RoomId = roomId;
            LevelIndex = levelIndex;
            Sin = sin;
            Accepted = accepted;
            Context = context;
        }
    }
}