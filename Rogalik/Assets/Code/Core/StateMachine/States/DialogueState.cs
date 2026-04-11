namespace Core
{
    public class DialogueState : IState
    {
        private readonly IGameStateMachine _gameStateMachine;

        public DialogueState(IGameStateMachine gameStateMachine)
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
