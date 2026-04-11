namespace Core
{
    public class StateFactory : IStateFactory
    {
        private readonly IDIService _dIService;

        public StateFactory(IDIService dIService) =>        
            _dIService = dIService;
        
        public TGameState GetState<TGameState>() where TGameState : class, IExitableState => 
         _dIService.Container.Resolve<TGameState>();
    }
}
