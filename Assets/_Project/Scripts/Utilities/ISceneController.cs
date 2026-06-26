using System.Threading;
using Cysharp.Threading.Tasks;
using TowerDefense.Assets._Project.Scripts.Utilities;

namespace TowerDefense.Scripts.Utilities
{
    public interface ISceneController
    {
        public SceneTransition Transition();
        public UniTask ExecuteTransition(SceneTransition controller, CancellationToken cancellation = default);

    }
}