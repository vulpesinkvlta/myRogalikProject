namespace Core
{
    public class GenerateLevelState : IState
    {
        private readonly IGameStateMachine _gameStateMachine;

        public GenerateLevelState(IGameStateMachine gameStateMachine)
        {
            _gameStateMachine = gameStateMachine;
        }
        public void Enter()
        {
            throw new System.NotImplementedException();
        }

        public void Exit()
        {
            throw new System.NotImplementedException();
        }
    }
}
