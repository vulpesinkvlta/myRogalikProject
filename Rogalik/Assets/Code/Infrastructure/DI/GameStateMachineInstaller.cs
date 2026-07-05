using GamePlay;
using System;
using Unity.Android.Gradle.Manifest;
using UnityEngine;
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
            Container.Bind<IDialogueUIService>().To<DialogueUIService>().AsSingle();
            Container.Bind<ISinChoiceUIService>().To<SinChoiceUIService>().AsSingle();
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
            Container.BindInterfacesAndSelfTo<LoadSceneState>().AsSingle();
            Container.BindInterfacesAndSelfTo<StartRunState>().AsSingle();
            Container.BindInterfacesAndSelfTo<MainMenuState>().AsSingle();
            Container.BindInterfacesAndSelfTo<GenerateLevelState>().AsSingle();
            Container.BindInterfacesAndSelfTo<PlayerDeathState>().AsSingle();
            Container.BindInterfacesAndSelfTo<LevelLoopState>().AsSingle();
            Container.BindInterfacesAndSelfTo<LevelCompleteState>().AsSingle();
            Container.BindInterfacesAndSelfTo<DialogueState>().AsSingle();
            Container.BindInterfacesAndSelfTo<ChoiceState>().AsSingle();
            Container.BindInterfacesAndSelfTo<BossFightState>().AsSingle();
        }
    }
}
