using System;
using UnityEngine;

public class Castle : MonoBehaviour, IDamagable
{
    [Header("Attributes")]
    [SerializeField] public int maxHealth;

    public static Castle Instance { private set; get; }

    public int CurrentHealth { private set; get; }
    public event Action<int> OnTakeDamage;
    public event Action OnCastleDestroyed;

    //[Header("References")]

    private void Awake()
    {
        Instance = this;
        CurrentHealth = maxHealth;
    }

    private void Start()
    {
        OnTakeDamage += Castle_OnTakeDamage;
    }

    private void Castle_OnTakeDamage(int health)
    {
        if(health <= 0)
        {
            OnCastleDestroyed?.Invoke();
        }
    }

    public void TakeDamage(int amount)
    {
        CurrentHealth -= amount;
        OnTakeDamage?.Invoke(CurrentHealth);
    }



}
