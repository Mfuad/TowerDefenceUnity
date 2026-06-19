using UnityEngine;

public class Bullet : MonoBehaviour, IBullet
{
    [Header("Attributes")]
    [SerializeField] private float bulletSpeed;

    private Transform target;
    private int damage;

    private void Update()
    {
        if (target == null)
        {
            Destroy(gameObject);
            return;
        }
        transform.position = transform.position + GetDirection(transform.position, target.position) * bulletSpeed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Enemy enemy = (Enemy)collision.GetComponent<IDamagable>();
        if (enemy != null)
        {
            enemy.TakeDamage(damage);
            Destroy(gameObject);
        }
    }

    public void Init(Transform target, int damage)
    {
        this.target = target;
        this.damage = damage;
        gameObject.SetActive(true);
    }

    private Vector3 GetDirection(Vector3 first, Vector3 second)
    {
        var header = second - first;
        return header / header.magnitude;
    }
}
