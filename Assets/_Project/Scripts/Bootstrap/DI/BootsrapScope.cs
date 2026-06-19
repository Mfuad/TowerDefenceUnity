using TowerDefense.Scripts.Utilities;
using TowerDefense.Scripts.Utilities.Core;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace TowerDefense.Assets._Project.Scripts.Bootstrap.DI
{

     class BootstrapScope : LifetimeScope
    {
        protected override void Configure(IContainerBuilder builder)
        {
            RegisterPersistents(builder);

            builder.RegisterEntryPoint<BootstrapFlow>(Lifetime.Singleton);
        }

        private void RegisterPersistents(IContainerBuilder builder)
        {
            builder.Register<AddressablesAssetLoader>(Lifetime.Singleton).As<IAssetLoader>();
        }
    }
}
