namespace Core
{
    public class GameEndState : IState
    {
        private readonly IGameStateMachine _gameStateMachine;

        public GameEndState(IGameStateMachine gameStateMachine)
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
