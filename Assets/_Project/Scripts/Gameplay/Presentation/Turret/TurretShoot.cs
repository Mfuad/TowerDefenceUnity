using UnityEngine;
using System;
using System.Collections;

 class TurretShoot : IShooter
{
    private Transform _bulletSpawnPosition;
    private Bullet _bullet;

    private Transform _target;
    public event Action OnShoot;

    private int _damage;
    private float _fireRate;


    public void Initialize(Transform target,Bullet bullet)
    {
        _target = target;
        _bullet = bullet;
    }


    public IEnumerator ShootCoroutine()
    {
        while (_target != null)
        {
            Shoot(_target);
            yield return new WaitForSeconds(_fireRate);
        } 
    }

    public void Shoot(Transform target)
    {
        IBullet bullet = UnityEngine.Object.Instantiate(_bullet);
        bullet.Init(target, _damage);
        OnShoot?.Invoke();
    }
    //private void UpdateAttributes(int level)
    //{
    //    _damage = baseDamage + (level - 1);
    //    _fireRate = baseFireRate / level;
    //}
    private void TurretLevel_OnLevelChanged(int level)
    {
        //UpdateAttributes(level);
    }
    private void TargetProvider_OnTargetChanged(Transform obj)
    {
        this._target = obj;
    }
}
