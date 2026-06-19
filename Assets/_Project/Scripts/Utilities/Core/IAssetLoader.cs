using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace TowerDefense.Scripts.Utilities.Core
{
    public interface IAssetLoader    
    {
        UniTask<T> GetAsync<T>(
            string address = default,
            CancellationToken cancellation = default
            );
        void Release<T>(string address);
    }
}
