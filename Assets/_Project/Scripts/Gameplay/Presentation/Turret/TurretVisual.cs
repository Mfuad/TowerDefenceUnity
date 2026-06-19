using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

 class TurretVisual : MonoBehaviour
{
    [Header("Attributes")]
    [SerializeField] private float rotationSpeed;

    [Header("References")]
    [SerializeField] private Transform rotationPoint;

    private const string ON_SHOOT = "OnShoot";

    private IShooter shooter;
    private ITargetProvider targetProvider;
    private Animator animator;
    private Transform target;


    private void Awake()
    {
        shooter = GetComponent<IShooter>();
        targetProvider = GetComponent<ITargetProvider>();

        animator = GetComponent<Animator>();
    }
    private void Start()
    {
        shooter.OnShoot += Shooter_OnShoot;
        //targetProvider.OnTargetChanged += TurretTarget_OnTargetChanged;
    }
    private void OnDestroy()
    {
        shooter.OnShoot -= Shooter_OnShoot;
        //targetProvider.OnTargetChanged -= TurretTarget_OnTargetChanged;
    }

    private void TurretTarget_OnTargetChanged(Transform target)
    {
        this.target = target;
    }

    private void Shooter_OnShoot()
    {
        animator.SetTrigger(ON_SHOOT);
    }

    private void Update()
    {
        if (target != null)
        {
            Quaternion targetRotation = Quaternion.LookRotation(rotationPoint.forward, (target.position - rotationPoint.position).normalized);
            rotationPoint.rotation = Quaternion.RotateTowards(rotationPoint.rotation, targetRotation, Time.deltaTime * rotationSpeed);
        }
    }
}
