using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using VContainer;
using VContainer.Unity;

namespace TowerDefense.Assets._Project.Scripts.Gameplay.Infrastructure.DI.Installers
{
    public class ConfigInstaller : IInstaller
    {
        private IList<ScriptableObject> _configs;
        public void Initialize(IList<ScriptableObject> configs) 
        { 
            _configs = configs;
        }
        public void Install(IContainerBuilder builder)
        {
            if (_configs is null) throw new ArgumentNullException("Configs is null");
            foreach (var config in _configs)
            {
                builder.RegisterInstance(config);
            }

        }
    }
}
