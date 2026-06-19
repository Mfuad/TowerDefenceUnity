using System.Collections.Generic;
using System.IO;
using System;
using UnityEngine;
using UnityEngine.Splines;
using Unity.VisualScripting;


//interface IEnemy :IDisposable
//{
//}


// abstract class Enemy : MonoBehaviour, IDisposable
//{
//     void SetActive(bool value) => gameObject.SetActive(value);

//     virtual void Dispose()
//    {
//        Destroy(gameObject);
//    }

//     static implicit operator GameObject(Enemy enemy) => enemy.gameObject;
//}



[RequireComponent(typeof(SplineAnimate))]
public class Enemy : MonoBehaviour, IDamagable, IPoolObject
{
    private static string CASTLE_LAYER = "Castle";

    private SplineAnimate _splineAnimate;

    private int currentHealth;

    public event Action<float> OnHealthChanged;

    private EnemyConfig _config;
    
    private int MaxHealth => _config.MaxHealth;
    private int GoldOnDeath => _config.GoldOnDeath;
    private int Damage => _config.Damage;

    public void Init(EnemyConfig config,SplineContainer path)
    {
        _config = config;
        _splineAnimate.Container = path;
    }

    private void Awake()
    {
        _splineAnimate = GetComponent<SplineAnimate>();
    }

    private void Start()
    {
        currentHealth = MaxHealth;
    }

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        if (currentHealth <= 0)
        {
            //LevelManager.Instance.OnEnemyKilled(GoldOnDeath);
            DestroySelf();
        }
        OnHealthChanged?.Invoke((float)currentHealth / MaxHealth);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.layer == LayerMask.GetMask(CASTLE_LAYER))
        {
            ((IDamagable)collision.transform).TakeDamage(Damage);
            DestroySelf();
        }
    }

    void IPoolObject.Enable()
    {
        Debug.Log("Enemy Enable");
        gameObject.SetActive(true);
        _splineAnimate.Play();
    }

    void IPoolObject.Disable()
    {
        Debug.Log("Enemy Disable");
        gameObject.SetActive(false);
    }

    void IPoolObject.Destroy()
    {
        Debug.Log("Enemy Destroy");
        DestroySelf();
    }
    public void DestroySelf()
    {
        Destroy(gameObject);
    }
}

//private void FixedUpdate()
//{
//    if (target == null) return;
//    rb.linearVelocity = GetDirection(transform.position, target.Value.position) * moveSpeed;
//}

//private float RotationAngle(Vector2 direction)
//{
//    return (float)(Math.Atan2(direction.y, direction.x) * (180 / Math.PI));
//}

//private Vector3 GetDirection(Vector3 first, Vector3 second)
//{
//    return (second - first).normalized;
//}