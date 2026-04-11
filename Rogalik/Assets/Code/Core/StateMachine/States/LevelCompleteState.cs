namespace Core
{
    public class LevelCompleteState : IState
    {
        private readonly IGameStateMachine _gameStateMachine;

        public LevelCompleteState(IGameStateMachine gameStateMachine)
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
