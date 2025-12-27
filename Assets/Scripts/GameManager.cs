using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Attributes")]
    [SerializeField] private float gameStartTimerMax;

    public static GameManager Instance { private set; get; }
    public event Action<State> OnStateChanged;
    public event Action OnGamePaused;

    private float gameStartTimer;
    public bool IsGamePaused { private set; get; }
    public enum State
    {
        gameStart,
        gameRunning,
        gameOver,
    }
    private State state;


    private void Awake()
    {
        Instance = this;
        state = State.gameStart;

        IsGamePaused = false;
    }

    private void Start()
    {
        Castle.Instance.OnCastleDestroyed += Instance_OnCastleDestroyed;
        GameInput.Instance.OnPauseAction += GameInput_OnPauseAction;
    }

    private void GameInput_OnPauseAction()
    {
        TogglePause();
    }

    private void Instance_OnCastleDestroyed()
    {
        state = State.gameOver;
        OnStateChanged?.Invoke(state);
    }

    private void Update()
    {
        switch (state)
        {
            case State.gameStart:
                gameStartTimer += Time.deltaTime;
                if (gameStartTimer < gameStartTimerMax) break;

                state = State.gameRunning;
                OnStateChanged?.Invoke(state);
                break;
            case State.gameRunning:
                break;
            case State.gameOver:
                break;
        }
    }


    public bool IsGameRunning()
    {
        return state == State.gameRunning;
    }
    public bool IsGameOver()
    {
        return state == State.gameOver;
    }

    public void TogglePause()
    {
        if (IsGamePaused)
        {
            Time.timeScale = 1;
        }
        else
        {
            Time.timeScale = 0;
        }
        IsGamePaused = !IsGamePaused;
        OnGamePaused?.Invoke();
    }

}
