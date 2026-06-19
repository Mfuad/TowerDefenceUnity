using System;
using System.Collections.Generic;
using UnityEngine;

public class TurretTarget : ITargetProvider
{
    private float _radius;
     float Radius
    {
        get { return _radius; }
        set 
        { 
            if (_radius <= 0) throw new ArgumentOutOfRangeException("Radius must be greater than 0");
            _radius = value; 
        }
    }

    private CircleCollider2D _circleCollider;
    private Transform _target;
    private List<Transform> _enemyList = new();

    public event Action<Transform> OnTargetChanged;


    private void Construct(CircleCollider2D circleCollider,MonoBehaviour context)
    {
        _circleCollider = circleCollider;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        _enemyList.Add(collision.transform);
        ChangeTarget();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        _enemyList.Remove(collision.transform);
        ChangeTarget();
    }

    private void ChangeTarget()
    {
        if (_enemyList.Count <= 0)
        {
            _target = null;
            OnTargetChanged?.Invoke(_target);
        }
        else if (_target != _enemyList[0])
        {
            _target = _enemyList[0];
            OnTargetChanged?.Invoke(_target);
        }
    }
}
