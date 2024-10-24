using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public static GameManager Instance { get => instance; }

    private ScoreManager scoreManager;
    private LivesManager livesManager;

    private bool isPlaying = true;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        scoreManager = FindObjectOfType<ScoreManager>();
        livesManager = FindObjectOfType<LivesManager>();
    }

    public void StartGame()
    {
        SceneManager.LoadScene("Gameplay");
    }

    public void ContinueGame()
    {
        SceneManager.LoadScene("Gameplay");
    }

    public void GameOver()
    {
        isPlaying = false;
        livesManager.GameOver();
        scoreManager.GameOver();
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void AddScore(int value)
    {
        scoreManager.AddScore(value);
    }

    public void LoseLife()
    {
        if (livesManager.IsAlive())
            livesManager.LoseLife();
        else
            GameOver();
    }

    public bool IsPlaying() => isPlaying;
}
