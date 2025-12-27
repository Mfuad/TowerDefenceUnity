using System.Collections.Generic;
using System.IO;
using System;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Enemy : MonoBehaviour, IDamagable
{
    [Header("Attributes")]
    [SerializeField] private float moveSpeed;
    [SerializeField] private int damage;
    [SerializeField] private int maxHealth;
    [SerializeField] private int goldOnKill;

    private LinkedList<Transform> path;
    private LinkedListNode<Transform> target;
    private LinkedListNode<Transform> current;

    private int currentHealth;

    public event Action<float> OnHealthChanged;

    private void Start()
    {
        currentHealth = maxHealth;
        path = LevelManager.Instance.Path;
    }

    private void Update()
    {
        target ??= path.First.Next;
        current ??= path.First;

        if (currentHealth <= 0)
        {
            LevelManager.Instance.OnEnemyKilled(goldOnKill);
            DestroySelf();
            return;
        }

        if (target == null)
        {
            DestroySelf();
            return;
        }
        if (Vector2.Distance(transform.position, target.Value.position) <= 0.01)
        {
            current = target;
            target = target.Next;
            transform.rotation = Quaternion.FromToRotation(Vector3.right, (target.Value.position - transform.position).normalized);
        }

        transform.position = transform.position + GetDirection(transform.position, target.Value.position) * Time.deltaTime * moveSpeed;
    }


    private float RotationAngle(Vector2 direction)
    {
        return (float)(Math.Atan2(direction.y, direction.x) * (180 / Math.PI));
    }

    private Vector3 GetDirection(Vector3 first, Vector3 second)
    {
        return (second - first).normalized;
    }

    private void DestroySelf()
    {
        EnemySpawner.Instance.OnEnemyDeath();
        Destroy(gameObject);
    }

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        OnHealthChanged?.Invoke((float)currentHealth / maxHealth);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.transform == Castle.Instance.transform)
        {
            Castle.Instance.TakeDamage(damage);
            DestroySelf();
        }
    }
}
