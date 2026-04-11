namespace Core
{
    public class LevelLoopState : IState
    {
        private readonly IGameStateMachine _gameStateMachine;

        public LevelLoopState(IGameStateMachine gameStateMachine)
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
