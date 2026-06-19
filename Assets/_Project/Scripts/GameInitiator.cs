using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;

 class GameInitiator : MonoBehaviour
{
    [SerializeField] private Camera mainCamera;
    [SerializeField] private Light2D globalLight2D;
    [SerializeField] private EventSystem eventSystem;
    [SerializeField] private Canvas canvas;
    [SerializeField] private Castle castle;
    [SerializeField] private EnemySpawner enemySpawner;
    [SerializeField] private GameInput gameInput;
    [SerializeField] private GameManager gameManager;
    //[SerializeField] private LevelManager levelManager;
    [SerializeField] private WaveManager waveManager;

    private void Awake()
    {
        BindObject();

        //await InitializeObject();
    }
    private void BindObject()
    {
        //mainCamera = Instantiate(mainCamera);
        //globalLight2D = Instantiate(globalLight2D);
        //eventSystem = Instantiate(eventSystem);
        //canvas = Instantiate(canvas);
        //gameInput = Instantiate(gameInput);
        //gameManager = Instantiate(gameManager);
        //SceneManager.LoadScene("Level 1", LoadSceneMode.Additive);
        //waveManager = Instantiate(waveManager);
        //enemySpawner = Instantiate(enemySpawner);
    }

    //private async Type InitializeObject()
    //{

    //}

}
