using System;

namespace GamePlay
{
    public interface IDialogueView
    {
        void Show(DialogueConfig dialogue, Action onCompleted);
        void Hide();    
    }
}
