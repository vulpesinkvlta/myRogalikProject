namespace Core
{
    public interface IStateFactory
    {
        TGameState GetState<TGameState>() where TGameState : class, IExitableState;
    }
}
