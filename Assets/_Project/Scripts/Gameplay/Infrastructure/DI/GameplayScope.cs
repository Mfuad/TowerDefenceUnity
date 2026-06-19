using System;
using System.Collections.Generic;
using TowerDefense.Assets._Project.Scripts.Gameplay.Core.Interfaces;
using TowerDefense.Assets._Project.Scripts.Gameplay.Domain;
using TowerDefense.Assets._Project.Scripts.Gameplay.Infrastructure.DI;
using TowerDefense.Assets._Project.Scripts.Gameplay.Infrastructure.DI.Installers;
using TowerDefense.Assets._Project.Scripts.Utilities;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;
using VContainer;
using VContainer.Unity;

class GameplayScope : LifetimeScope
{
    [SerializeField] private Canvas _canvas;
    [SerializeField] private ConfigInstaller _configInstaller = new();


    protected override void Configure(IContainerBuilder builder)
    {

        builder.Register<ConfigProvider>(Lifetime.Singleton);
        //builder.Register<SceneLoader>(Lifetime.Singleton);

        builder.Register<GameInput>(Lifetime.Singleton);
        builder.Register<LevelManager>(Lifetime.Singleton);
        builder.Register<WaveManager>(Lifetime.Singleton).AsSelf();
        builder.Register<GameManager>(Lifetime.Singleton);

        builder.RegisterInstance(_canvas);

        builder.Register<EnemySpawner>(Lifetime.Singleton);

        builder.Register(container =>
        {
            return new IPathUser[]
            {
                container.Resolve<EnemySpawner>()
            };
        }, Lifetime.Singleton).As<IReadOnlyList<IPathUser>>();

        builder.RegisterEntryPoint<GameplayFlow>(Lifetime.Singleton);
    }
}

