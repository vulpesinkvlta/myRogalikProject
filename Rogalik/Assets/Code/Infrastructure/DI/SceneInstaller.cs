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
    }

    private void LevelBind()
    {
        Container.BindInstance(_levelConfig).AsSingle();
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