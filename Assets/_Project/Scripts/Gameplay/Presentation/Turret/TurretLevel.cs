using System;
using UnityEngine;

[RequireComponent(typeof(TurretUI))]
public class TurretLevel : MonoBehaviour
{
    [Header("Attributes")]
    [SerializeField] private int baseLevelUpPrice;

    public int Level { private set; get; }
    public int LevelUpPrice { private set; get; }

    private TurretUI turretUI;
    public Action<int> OnLevelChanged;

    private void Awake()
    {
        turretUI = GetComponent<TurretUI>();
        Level = 1;
        LevelUpPrice = baseLevelUpPrice;
    }

    private void Start()
    {
        turretUI.OnLevelUpButtonPressed += TurretUI_OnLevelUpButtonPressed;
    }
    private void OnDestroy()
    {
        turretUI.OnLevelUpButtonPressed -= TurretUI_OnLevelUpButtonPressed;
    }

    void TurretUI_OnLevelUpButtonPressed()
    {
        if (TryLevelUp())
        {

        }
    }

    public bool TryLevelUp()
    {
        //if (LevelManager.Instance.TrySpendGold(LevelUpPrice))
        //{
        //    Level++;
        //    UpdateAttributes(Level);
        //    OnLevelChanged?.Invoke(Level);
        //    return true;
        //}
        return false;
    }

    private void UpdateAttributes(int level)
    {
        LevelUpPrice = baseLevelUpPrice * Level;
    }
}
