using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class TurretVisual : MonoBehaviour
{
    [Header("Attributes")]
    [SerializeField] private float rotationSpeed;

    [Header("References")]
    [SerializeField] private Transform rotationPoint;

    private const string ON_SHOOT = "OnShoot";

    private Turret turret;
    private Animator animator;
    private Transform target;


    private void Awake()
    {
        turret = GetComponent<Turret>();
        animator = GetComponent<Animator>();
    }
    private void Start()
    {
        turret.OnShoot += Turret_OnShoot;
        turret.OnTargetChanged += Turret_OnTargetChanged;
    }

    private void Turret_OnTargetChanged(Transform target)
    {
        this.target = target;
    }

    private void Turret_OnShoot()
    {
        animator.SetTrigger(ON_SHOOT);
    }

    private void FixedUpdate()
    {
        if (target != null)
        {
            Quaternion targetRotation = Quaternion.LookRotation(rotationPoint.forward, (target.position - rotationPoint.position).normalized);
            rotationPoint.rotation = Quaternion.RotateTowards(rotationPoint.rotation, targetRotation, Time.deltaTime * rotationSpeed);
        }
    }

    private float AngleBetweenAxis(Vector3 first, Vector3 second)
    {
        return (float)(Math.Atan2(first.x, second.y) * (180 / Math.PI));
    }

    private Vector3 GetDirection(Vector3 first, Vector3 second)
    {
        return (second - first).normalized;
    }
}
