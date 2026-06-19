using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(TurretLevel))]
public class TurretUI : MonoBehaviour, IPointerClickHandler
{
    [Header("References")]
    [SerializeField] private Button levelUpButton;
    [SerializeField] private TextMeshProUGUI levelUpText;
    [SerializeField] private GameObject popup;

    private TurretLevel turretLevel;
    public event Action OnLevelUpButtonPressed;

    private void Awake()
    {
        turretLevel = GetComponent<TurretLevel>();
    }

    private void Start()
    {
        levelUpButton.onClick.AddListener(()=> {
            OnLevelUpButtonPressed.Invoke();
        });
        turretLevel.OnLevelChanged += TurretLevel_OnLevelChanged;
        

        HideUI();
    }

    private void OnDestroy()
    {
        turretLevel.OnLevelChanged -= TurretLevel_OnLevelChanged;
    }

    void TurretLevel_OnLevelChanged(int level)
    {
        levelUpText.text = $"LEVEL {level + 1}\n{turretLevel.LevelUpPrice}";
    }

    private void ShowUI()
    {
        popup.SetActive(true);
    }

    private void HideUI()
    {
        popup.SetActive(false);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        popup.SetActive(!popup.activeSelf);
    }
}
