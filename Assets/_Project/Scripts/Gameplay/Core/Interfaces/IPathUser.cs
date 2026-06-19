using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine.Splines;

namespace TowerDefense.Assets._Project.Scripts.Gameplay.Core.Interfaces
{
    public interface IPathUser
    {
        void RegisterPath(SplineContainer path);
        void UnregisterPath();
    }
}
