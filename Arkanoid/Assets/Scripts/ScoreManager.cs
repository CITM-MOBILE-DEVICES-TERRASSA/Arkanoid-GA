using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    private static ScoreManager instance;
    public static ScoreManager Instance { get => instance; }

    [Header("UI")]
    [SerializeField] private Transform scorePanel;
    [SerializeField] private Sprite[] numberSprites;
    [SerializeField] private GameObject digitPrefab;
    [Header("Audio")]
    [SerializeField] private AudioClip scoreFx;

    private int score = 0;
    private int highScore = 0;

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

        UpdateScoreText();
    }

    public void AddScore(int value)
    {
        score += value;
        UpdateScoreText();
        AudioManager.Instance.PlayFx(scoreFx);
        CheckIfHighScore();
    }

    private void CheckIfHighScore()
    {
        if (score > highScore)
        {
            highScore = score;
        }
    }

    private void UpdateScoreText()
    {
        string scoreString = score.ToString();
        int currentDigitCount = scorePanel.childCount;

        if (scoreString.Length > currentDigitCount)
        {
            for (int i = currentDigitCount; i < scoreString.Length; i++)
            {
                Instantiate(digitPrefab, scorePanel);
            }
        }

        for (int i = 0; i < scoreString.Length; i++)
        {
            int digitValue = int.Parse(scoreString[i].ToString());
            scorePanel.GetChild(i).GetComponent<Image>().sprite = numberSprites[digitValue];
        }
    }
}
