namespace Core
{
   public struct DialogueEndedEvent
   {
        public readonly DialogueConfig Dialogue;

        public DialogueEndedEvent(DialogueConfig dialogue)
        {
            Dialogue = dialogue;
        }
    }
}
