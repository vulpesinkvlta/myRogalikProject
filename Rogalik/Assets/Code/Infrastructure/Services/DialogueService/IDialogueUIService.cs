using GamePlay;

namespace Core
{
    public interface IDialogueUIService
    {
        void Register(IDialogueView view);
        void Unregister(IDialogueView view);
        void Show(DialogueConfig dialogue, System.Action onCompleted);
        void Hide();
    }
}
