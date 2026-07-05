using GamePlay;
using System;
using UnityEngine;

namespace Core
{
    public class SinChoiceUIService : ISinChoiceUIService
    {
        private ISinChoiceView _view;

        public void Register(ISinChoiceView view)
        {
            _view = view;
        }

        public void Unregister(ISinChoiceView view)
        {
            if (_view == view)
                _view = null;
        }

        public void Show(SinsConfig sin, Action onAccept, Action onRefuse)
        {
            if (_view == null)
            {
                Debug.LogError("SinChoiceView is not registered");
                return;
            }

            _view.Show(sin, onAccept, onRefuse);
        }

        public void Hide()
        {
            _view?.Hide();
        }
    }
}