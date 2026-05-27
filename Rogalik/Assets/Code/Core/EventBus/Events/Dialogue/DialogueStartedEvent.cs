namespace Core
{
    public struct DialogueStartedEvent
    {
        public DialogueConfig Dialogue;

        public DialogueStartedEvent(DialogueConfig dialogue)
        {
            Dialogue = dialogue;
        }
    }
}
