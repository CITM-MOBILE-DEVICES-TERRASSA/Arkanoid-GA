using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LivesManager : MonoBehaviour
{
    [Header("Lives")]
    [SerializeField] private int initialLives = 3;
    [Header("UI")]
    [SerializeField] private Transform livesPanel;
    [SerializeField] private GameObject lifePrefab;
    [SerializeField] private GameObject gameOverPanel;
    [Header("Audio")]
    [SerializeField] private AudioClip loseLifeFx;

    private int currentLives;
    private List<GameObject> lifeIcons = new List<GameObject>();

    private void Awake()
    {
        currentLives = initialLives;
        InitializeLivesUI();
    }

    private void InitializeLivesUI()
    {
        for (int i = 0; i < initialLives; i++)
        {
            GameObject life = Instantiate(lifePrefab, livesPanel);
            lifeIcons.Add(life);
        }
    }

    private void UpdateLivesUI()
    {
        for (int i = lifeIcons.Count - 1; i >= currentLives; i--)
        {
            Destroy(lifeIcons[i]);
            lifeIcons.RemoveAt(i);
        }
    }

    public void LoseLife()
    {
        currentLives--;
        UpdateLivesUI();
        AudioManager.Instance.PlayFx(loseLifeFx);
    }

    public bool IsAlive() => currentLives > 1;
    public void GameOver() => gameOverPanel.SetActive(true);
}