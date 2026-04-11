using Zenject;

namespace Core
{
    public class DIService : IDIService
    {
        private DiContainer _container;

        public DIService()
        {
            _container = ProjectContext.Instance.Container;
        }

        public DiContainer Container => _container;
    }
}