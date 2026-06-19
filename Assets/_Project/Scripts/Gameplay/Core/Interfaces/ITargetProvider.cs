using System;
using UnityEngine;

 interface ITargetProvider
{
     event Action<Transform> OnTargetChanged;
}
