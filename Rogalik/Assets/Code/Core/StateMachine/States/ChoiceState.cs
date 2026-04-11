namespace Core
{
    public class ChoiceState : IState
    {
        private readonly IGameStateMachine _gameStateMachine;

        public ChoiceState(IGameStateMachine gameStateMachine)
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
