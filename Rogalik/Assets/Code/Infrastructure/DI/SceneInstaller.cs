using Core;
using GamePlay;
using System;
using UnityEngine;
using Zenject;

public class SceneInstaller : MonoInstaller
{
    [SerializeField] private PlayerConfig _playerConfig;
    [SerializeField] private LevelConfig _levelConfig;
    public override void InstallBindings()
    {
        InputBind();
        PlayerBind();
        LevelBind();
        DialogueBind();
        SinBind();
    }

    private void SinBind()
    {
        Container.Bind<ISinChoiceView>().To<SinChoiceView>().FromComponentInHierarchy().AsSingle();
        Container.BindInterfacesAndSelfTo<ChoiceState>().AsSingle();
        Container.BindInterfacesAndSelfTo<SinFlowController>().AsSingle();
    }

    private void DialogueBind()
    {
        Container.Bind<IDialogueView>().To<DialogueView>().FromComponentInHierarchy().AsSingle();
    }

    private void LevelBind()
    {
        Container.BindInstance(_levelConfig).AsSingle().NonLazy();
        Container.Bind<CurrentLevelContext>().AsSingle();
    }

    private void InputBind()
    {
        Container.BindInterfacesAndSelfTo<KeyboardInputService>().AsSingle().NonLazy();
    }
    private void PlayerBind()
    {
        Container.BindInstance(_playerConfig).AsSingle();
        Container.Bind<PlayerData>().AsSingle();        
    }
}