using GamePlay;
using System;

namespace Core
{
    public class DialogueUIService : IDialogueUIService
    {
        private IDialogueView _view;

        public void Register(IDialogueView view)
        {
            _view = view;
        }

        public void Unregister(IDialogueView view)
        {
            if (_view == view)
                _view = null;
        }

        public void Show(DialogueConfig dialogue, Action onCompleted)
        {
            if (_view == null)
            {
                UnityEngine.Debug.LogError("DialogueView is not registered");
                return;
            }

            _view.Show(dialogue, onCompleted);
        }

        public void Hide()
        {
            _view?.Hide();
        }
    }
}