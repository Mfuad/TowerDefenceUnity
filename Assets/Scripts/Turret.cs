using System;
using System.Collections.Generic;
using UnityEngine;


public class Turret : MonoBehaviour
{
    [Header("Attributes")]
    [SerializeField] private int baseDamage;
    [SerializeField] private float baseFireRate;
    [SerializeField] private float baseRadius;
    [SerializeField] private int baseLevelUpPrice;
    [SerializeField] private float perLevelIncrease;


    [Header("References")]
    [SerializeField] private Transform firePoint;
    [SerializeField] private Bullet bulletPrefab;
    [SerializeField] private LayerMask enemyLayerMask;

    public event Action<Transform> OnTargetChanged;
    public event Action OnShoot;

    private LinkedList<Transform> enemyList;

    private Transform target;
    private CircleCollider2D circleCollider;

    public int Level { private set; get; }
    public int LevelUpPrice { private set; get; }

    private int damage;
    private float fireRate;
    private float fireTimer;

    private void Awake()
    {
        circleCollider = GetComponent<CircleCollider2D>();
        enemyList = new();
        Level = 1;
        UpdateLevelAttributes();
    }

    private void Update()
    {
        if (fireRate < fireTimer && target != null)
        {
            Shoot(target);
            fireTimer = 0;
        }
        fireTimer += Time.deltaTime;

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        enemyList.AddLast(collision.transform);
        OnEnemyStackChange();
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        enemyList.Remove(collision.transform);
        OnEnemyStackChange();
    }

    private void OnEnemyStackChange()
    {
        if (enemyList.Count <= 0)
        {
            target = null;
            OnTargetChanged?.Invoke(target);
        }
        else if (target != enemyList.First.Value)
        {
            target = enemyList.First.Value;
            OnTargetChanged?.Invoke(target);
        }
    }

    private void Shoot(Transform target)
    {
        Bullet bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        bullet.SetDamage(damage);
        bullet.SetTarger(target);
        bullet.Enable();
        OnShoot?.Invoke();
    }

    public bool TryLevelUp()
    {
        if (LevelManager.Instance.TrySpendGold(LevelUpPrice))
        {
            Level++;
            UpdateLevelAttributes();
            return true;
        }
        return false;
    }

    private void UpdateLevelAttributes()
    {
        damage = baseDamage + (Level - 1);
        fireRate = baseFireRate / Level;
        circleCollider.radius = baseRadius + (Level - 1);
        LevelUpPrice = baseLevelUpPrice * Level;
    }

    //private float GetTargetRadious(Transform target)
    //{
    //    return target.GetComponent<CircleCollider2D>().radius;
    //}

    //private void OnDrawGizmosSelected()
    //{
    //    Gizmos.color = Color.yellow;
    //    Gizmos.DrawWireSphere(transform.position, range);
    //}
}
