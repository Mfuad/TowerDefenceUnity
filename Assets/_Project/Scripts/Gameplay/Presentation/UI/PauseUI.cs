using UnityEngine;
using UnityEngine.UI;

 class PauseUI : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Button resumeButton;

    private void Start()
    {
        GameManager.Instance.OnGamePaused += GameManager_OnGamePaused;
        resumeButton.onClick.AddListener(() =>
        {
            GameManager.Instance.TogglePause();
        });

        Hide();
    }

    private void GameManager_OnGamePaused()
    {
        gameObject.SetActive(GameManager.Instance.IsGamePaused);
    }

    private void Show()
    {
        gameObject.SetActive(true);
    }
    private void Hide()
    {
        gameObject.SetActive(false);
    }
}
