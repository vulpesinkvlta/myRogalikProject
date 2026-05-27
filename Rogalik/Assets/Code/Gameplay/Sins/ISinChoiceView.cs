using System;

namespace GamePlay
{
    public interface ISinChoiceView
    {
        void Show(SinsConfig sin, Action onAccept, Action onRefuse);
        void Hide();
    }
}
