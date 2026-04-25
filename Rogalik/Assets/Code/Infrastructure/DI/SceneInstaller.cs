using Core;
using System;
using UnityEngine;
using Zenject;

public class SceneInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        InputInstall();
    }

    private void InputInstall()
    {
        Container.BindInterfacesAndSelfTo<KeyboardInputService>().AsSingle().NonLazy();
    }
}