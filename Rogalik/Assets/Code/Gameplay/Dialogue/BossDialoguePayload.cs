namespace Core
{
    public readonly struct BossDialoguePayload
    {
        public readonly int RoomId;
        public readonly int LevelIndex;
        public readonly DialogueConfig Dialogue;
        public readonly BossConfig Boss;
        public readonly SinsConfig Sin;
        public BossDialoguePayload(
            int roomId,
            int levelIndex, 
            BossConfig boss, 
            SinsConfig sin,
            DialogueConfig dialogue
            )
        {
            Dialogue = dialogue;
            Boss = boss;
            Sin = sin;
            LevelIndex = levelIndex;
            RoomId = roomId;
        }
    }
}
