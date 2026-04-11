using Zenject;

namespace Core
{
    public class GameBootstrapper : IInitializable
    {
        private readonly IGameStateMachine _stateMachine;

        public GameBootstrapper(IGameStateMachine stateMachine)
        {
            _stateMachine = stateMachine;
        }
        public void Initialize()
        {
            _stateMachine.Enter<BootStrapState>();  
        }
    }
}
