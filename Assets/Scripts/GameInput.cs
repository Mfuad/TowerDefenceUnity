using System;
using UnityEngine;

public class GameInput : MonoBehaviour
{

    public static GameInput Instance { private set; get; }

    private GameInputAction gameInputAction;
    public event Action OnPauseAction;


    private void Awake()
    {
        Instance = this;
        gameInputAction = new();

        gameInputAction.Player.Enable();
    }
    private void Start()
    {
        gameInputAction.Player.Pause.performed += Pause_performed;
    }

    private void OnDestroy()
    {
        gameInputAction.Player.Pause.performed -= Pause_performed;
        gameInputAction.Player.Disable();
    }

    private void Pause_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnPauseAction?.Invoke();
    }
}
