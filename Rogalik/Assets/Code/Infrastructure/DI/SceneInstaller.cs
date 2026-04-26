using Core;
using System;
using UnityEngine;
using Zenject;

public class SceneInstaller : MonoInstaller
{
    [SerializeField] private PlayerConfig playerConfig;
    public override void InstallBindings()
    {
        InputBind();
        PlayerBind();
    }

    private void InputBind()
    {
        Container.BindInterfacesAndSelfTo<KeyboardInputService>().AsSingle().NonLazy();
    }
    private void PlayerBind()
    {
        Container.BindInstance(playerConfig).AsSingle();
        Container.Bind<PlayerData>().AsSingle();        
    }
}