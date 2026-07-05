using GamePlay;

namespace Core
{
    public interface ISinChoiceUIService
    {
        void Register(ISinChoiceView view);
        void Unregister(ISinChoiceView view);
        void Show(SinsConfig sin, System.Action onAccept, System.Action onRefuse);
        void Hide();
    }
}