namespace Core
{
    public class GamePauseState : IState
    {
        private readonly IGameStateMachine _gameStateMachine;

        public GamePauseState(IGameStateMachine gameStateMachine)
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
