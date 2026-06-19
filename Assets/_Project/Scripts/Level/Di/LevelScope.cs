using UnityEditor;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.Splines;
using VContainer;
using VContainer.Unity;

namespace TowerDefense.Assets._Project.Scripts.Level.Di
{
    public class LevelScope : LifetimeScope
    {
        [SerializeField] private SplineContainer _path;

        public SplineContainer LoadPathFromScene()
        {
            return _path;
        }
    }
}
