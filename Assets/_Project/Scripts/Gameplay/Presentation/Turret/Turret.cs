using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(IShooter))]
 class Turret : MonoBehaviour
{
    private IShooter shooter = new TurretShoot();
    private Coroutine shootCoroutine;
    private ITargetProvider targetProvider = new TurretTarget();

    private void Awake()
    {
        shooter = GetComponent<IShooter>();
    }

    private void Start()
    {
        targetProvider.OnTargetChanged += TargetProvider_OnTargetChanged;

        StopAllCoroutines();
        shootCoroutine = StartCoroutine(shooter.ShootCoroutine());
    }

    private void TargetProvider_OnTargetChanged(Transform target)
    {
        if(target == null && shootCoroutine != null)
        {
            StopCoroutine(shootCoroutine);
            shootCoroutine = null;
        }
        if (shootCoroutine == null && target != null)
        {
            shootCoroutine = StartCoroutine(shooter.ShootCoroutine());
        }
    }

    private void OnDestroy()
    {
        StopAllCoroutines();
    }
}
