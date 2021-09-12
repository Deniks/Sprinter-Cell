using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    InputManager inputManager;
    [SerializeField]
    GameOverPanelController gameOverPanelController;

    [SerializeField]
    PlatformManager platformManager;

    RunTimer timer;

    public static bool isGameOver = false;

    public static int playerCoins = 0;
    public static bool collectables = true;
    public static bool isPaused = false;
    public static bool respawnGranted = false;

    public static string levelDifficulty = "easy";

    [SerializeField]
    private int minCoinsToContinue = 5;
    [SerializeField] 
    private GameObject pausePanel;

    [SerializeField] 
    private GameObject gameOverPanel;


    [SerializeField]
    private GameObject mainGamePanel;

    void Start()
    {
        inputManager = FindObjectOfType<InputManager>();
        gameOverPanelController = GetComponent<GameOverPanelController>();
        platformManager = GetComponent<PlatformManager>(); 
        timer = GetComponent<RunTimer>();
        mainGamePanel.SetActive(true);
        gameOverPanel.SetActive(false);
        pausePanel.SetActive(false);
        playerCoins = PlayerPrefsManager.GetCoins();
        timer.ResetTimer();
    }
    void Update()
    {
        // difficulty settings
        if (timer.GetTimerValue() > 10)
        {
            levelDifficulty = "medium";
        }
        else if (timer.GetTimerValue() > 20)
        {
            levelDifficulty = "hard";
        }


        Debug.Log(levelDifficulty);


        if (inputManager.escInput && !isGameOver)
        {
            inputManager.escInput = false;
            Debug.Log(pausePanel.activeInHierarchy);
            if (!pausePanel.activeInHierarchy)
            {
                PauseGame();
            }
            else if (pausePanel.activeInHierarchy)
            {
                ContinueGame();
            }
        }

        if (isGameOver)
        {
            HandleGameOver();
        }
        
    }
    public void PauseGame()
    {
        Debug.Log("Pause");

        Time.timeScale = 0;
        isPaused = true;
        pausePanel.SetActive(true);
    }
    public void ContinueGame()
    {
        Time.timeScale = 1;
        isPaused = false;
        pausePanel.SetActive(false);
    }

    public void TogglePaue()
    {
        if (isPaused)
        {
            ContinueGame();
        }
        else
        {
            PauseGame();
        }
    }

    public void RestartGame()
    {
        Time.timeScale = 1;
        isGameOver = false;
        platformManager.ResetPlatforms();
        gameOverPanel.SetActive(false);
        mainGamePanel.SetActive(true);
        timer.ResetTimer();
    }

    private void HandleGameOver() {
        Time.timeScale = 0;

        mainGamePanel.SetActive(false);
        gameOverPanel.SetActive(true);
        Debug.Log("GameOver");
        PlayerPrefsManager.SetCoins(playerCoins);

        if (timer.GetTimerValue() > PlayerPrefsManager.GetBestScore())
        {
            PlayerPrefsManager.SetScore(timer.GetTimerValue());
        }

        gameOverPanelController.HandleUI(15, timer.GetFormattedTimerValue(), PlayerPrefsManager.GetBestScore());


    }


    public void HandleContinuationForCoins()
    {
        if (playerCoins >= minCoinsToContinue)
        {
            Time.timeScale = 1;
            platformManager.ResetPlatforms();
            gameOverPanel.SetActive(false);
            mainGamePanel.SetActive(true);

            isGameOver = false;
            respawnGranted = true;
            PlayerPrefsManager.SetCoins(-minCoinsToContinue);
            Debug.Log("Continue");
            platformManager.ResetPlatforms();
        }
        else
        {
            Debug.Log("Not enough coins");
        }
    }

}
