using Zenject;

namespace Core
{
    public class GameStateMachine : IGameStateMachine, ITickable
    {
        private readonly IStateFactory _stateFactory;
        private IExitableState _currentState;

        public GameStateMachine(IStateFactory stateFactory)
        {
            _stateFactory = stateFactory;
        }
        void ITickable.Tick()
        {
            if (_currentState is ITickable tickable)
                tickable.Tick();
        }

        public void Enter<TState>() where TState : class, IState
        {
            IState state = ChangeState<TState>();
            state.Enter();
        }

        public void Enter<TState, TPayload>(TPayload payload) where TState : class, IPayloadedState<TPayload>
        {
            TState state = ChangeState<TState>();
            state.Enter(payload);
        }
        private TState ChangeState<TState>() where TState : class, IExitableState
        {
            _currentState?.Exit();

            TState state = GetState<TState>();
            _currentState = state;
            return state;
        }

        private TState GetState<TState>() where TState : class, IExitableState =>
          _stateFactory.GetState<TState>();
    }
}
