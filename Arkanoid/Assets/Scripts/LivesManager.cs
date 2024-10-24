using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LivesManager : MonoBehaviour
{
    private static LivesManager instance;
    public static LivesManager Instance { get => instance; }

    [Header("Lives")]
    [SerializeField] private int initialLives = 3;
    [Header("UI")]
    [SerializeField] private Transform livesPanel;
    [SerializeField] private GameObject lifePrefab;

    private int currentLives;
    private List<GameObject> lifeIcons = new List<GameObject>();

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
        if (currentLives > 0)
        {
            currentLives--;
            UpdateLivesUI();
        }
    }

    public bool IsAlive() => currentLives > 0;
}