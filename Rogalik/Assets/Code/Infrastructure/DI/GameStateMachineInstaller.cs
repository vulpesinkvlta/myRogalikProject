using System;
using Unity.Android.Gradle.Manifest;
using Zenject;

namespace Core
{
    public class GameStateMachineInstaller : MonoInstaller, ICoroutineRunner
    {
        public override void InstallBindings()
        {
            BindServices();
            BindEventBus();
            BindStateMachine();
        }

            

        private void BindServices()
        {
            Container.Bind<IDIService>().To<DIService>().AsSingle();
            Container.Bind<ICoroutineRunner>().FromInstance(this).AsSingle();
            Container.Bind<ISceneLoaderService>().To<SceneLoaderService>().AsSingle();
        }

        private void BindEventBus()
        {
            Container.Bind<IEventBus>().To<EventBus>().AsSingle();
        }

        private void BindStateMachine()
        {
            Container.Bind<IStateFactory>().To<StateFactory>().AsSingle();
            Container.BindInterfacesAndSelfTo<GameStateMachine>().AsSingle();
            Container.BindInterfacesAndSelfTo<BootStrapState>().AsSingle();
            Container.BindInterfacesAndSelfTo<LoadProgressState>().AsSingle();
            Container.BindInterfacesAndSelfTo<MainMenuState>().AsSingle();
        }
    }
}
