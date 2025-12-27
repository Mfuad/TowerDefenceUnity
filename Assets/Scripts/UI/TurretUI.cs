using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TurretUI : MonoBehaviour, IPointerClickHandler
{
    [Header("References")]
    [SerializeField] private Button levelUpButton;
    [SerializeField] private TextMeshProUGUI levelUpText;
    [SerializeField] private Turret turret;
    [SerializeField] private GameObject popup;

    private void Start()
    {
        levelUpButton.onClick.AddListener(OnLevelUpButtonPressed);
        levelUpText.text = $"LEVEL {turret.Level+1}\n{turret.LevelUpPrice}";

        HideUI();
    }

    private void OnLevelUpButtonPressed()
    {
        Debug.Log("rabotaet nigger");
        if (turret.TryLevelUp())
        {
            levelUpText.text = $"LEVEL {turret.Level + 1}\n{turret.LevelUpPrice}";
        }
    }

    private void ShowUI()
    {
        popup.SetActive(true);
    }
    private void HideUI()
    {
        popup.SetActive(false);
    }

    //public void OnMouseDown()
    //{
    //    Debug.Log("click");
    //    popup.SetActive(!popup.activeSelf);
    //}

    public void OnPointerClick(PointerEventData eventData)
    {
        popup.SetActive(!popup.activeSelf);
    }
}
