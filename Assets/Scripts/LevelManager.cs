using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class LevelManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform[] pathList;

    public static LevelManager Instance;
    public event Action<int> OnBalanceChanged;
    public LinkedList<Transform> Path { private set; get; }
    public int Balance { private set; get; }
    public int TotalKill { private set; get; }

    private void Awake()
    {
        Instance = this;
        Path = new(pathList);
    }

    public void OnEnemyKilled(int goldOnKill)
    {
        TotalKill++;
        Balance += goldOnKill;
        OnBalanceChanged?.Invoke(Balance);
    }

    public bool TrySpendGold(int amount)
    {
        if (Balance < amount) return false;

        Balance -= amount;
        OnBalanceChanged?.Invoke(Balance);
        return true;
    }
}
