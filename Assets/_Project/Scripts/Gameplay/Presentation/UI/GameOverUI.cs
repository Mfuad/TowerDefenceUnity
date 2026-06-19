using TMPro;
using UnityEngine;
using UnityEngine.UI;

 class GameOverUI : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private TextMeshProUGUI wave;
    [SerializeField] private TextMeshProUGUI kill;
    [SerializeField] private Button mainMenuButton;
    [SerializeField] private Button restartButton;

    private void Start()
    {
        GameManager.Instance.OnStateChanged += GameManager_OnStateChanged;

        mainMenuButton.onClick.AddListener(() =>
        {
            Loader.Load(Loader.Scene.MainMenuScene);
        });
        restartButton.onClick.AddListener(() =>
        {
            Loader.Load(Loader.Scene.GameScene);
        });

        Hide();
    }

    private void GameManager_OnStateChanged(GameManager.State obj)
    {
        if (GameManager.Instance.IsGameOver())
        {
            UpdateUI();
            Show();
        }
    }

    private void UpdateUI()
    {
        //wave.text = EnemySpawner.Instance.Wave.ToString();
        //kill.text = LevelManager.Instance.TotalKill.ToString();
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
