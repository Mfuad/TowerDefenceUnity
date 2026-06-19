using System.Collections;
using System;
using UnityEngine;

 interface IShooter 
{
     IEnumerator ShootCoroutine();
     void Shoot(Transform target);
     event Action OnShoot;
}
