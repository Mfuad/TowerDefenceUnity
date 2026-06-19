using System.Collections.Generic;
using TowerDefense.Assets._Project.Scripts.Gameplay.Core.Interfaces;
using UnityEngine;
using UnityEngine.Splines;
using VContainer;

namespace TowerDefense.Assets._Project.Scripts.Level
{

    [RequireComponent(typeof(SplineContainer))]
    public class SplineAnchor : MonoBehaviour
    {
        public IReadOnlyList<IPathUser> _pathUserList;

        [Inject]
        public void Construct(IReadOnlyList<IPathUser> pathUserList)
        {
            _pathUserList = pathUserList;
        }

        private void Start()
        {
            foreach (var pathUser in _pathUserList)
            {
                pathUser.RegisterPath(GetComponent<SplineContainer>());
            }
        }

        private void OnDisable()
        {
            foreach (var pathUser in _pathUserList)
            {
                pathUser.UnregisterPath();
            }
        }
    }
}
