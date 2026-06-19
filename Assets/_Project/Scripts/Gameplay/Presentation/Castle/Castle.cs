using System;
using UnityEngine;

public class Castle : MonoBehaviour, IDamagable
{
    public event Action<int> OnTakeDamage;
    public void TakeDamage(int amount)
    {
        OnTakeDamage?.Invoke(amount);
    }
}
