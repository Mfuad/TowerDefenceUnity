using UnityEngine;

public class Bullet : MonoBehaviour
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
        ;

        transform.position = transform.position + GetDirection(transform.position, target.position) * bulletSpeed;
        if (Vector2.Distance(target.position, transform.position) <= 0.1)
        {
            target.GetComponent<IDamagable>().TakeDamage(damage);
            Destroy(gameObject);
        }
    }

    public void SetTarger(Transform target)
    {
        this.target = target;
    }

    public void Enable()
    {
        gameObject.SetActive(true);
    }

    public void SetDamage(int damage)
    {
        this.damage = damage;
    }

    private Vector3 GetDirection(Vector3 first, Vector3 second)
    {
        var header = second - first;
        return header / header.magnitude;
    }
}
