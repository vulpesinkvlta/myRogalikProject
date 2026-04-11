namespace Core
{
    public class BossFightState : IState
    {
        private readonly IGameStateMachine _gameStateMachine;

        public BossFightState(IGameStateMachine gameStateMachine)
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
