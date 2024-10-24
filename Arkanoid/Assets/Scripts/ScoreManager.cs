using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private GameObject scorePanel;
    [SerializeField] private Transform digitsPanel;
    [SerializeField] private Sprite[] numberSprites;
    [SerializeField] private GameObject digitPrefab;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI highScoreText;
    [Header("Audio")]
    [SerializeField] private AudioClip scoreFx;

    private int score = 0;
    private int highScore = 0;

    private void Awake()
    {
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
        int currentDigitCount = digitsPanel.childCount;

        if (scoreString.Length > currentDigitCount)
        {
            for (int i = currentDigitCount; i < scoreString.Length; i++)
            {
                Instantiate(digitPrefab, digitsPanel);
            }
        }

        for (int i = 0; i < scoreString.Length; i++)
        {
            int digitValue = int.Parse(scoreString[i].ToString());
            digitsPanel.GetChild(i).GetComponent<Image>().sprite = numberSprites[digitValue];
        }
    }

    public void GameOver()
    {
        scorePanel.SetActive(false);
        scoreText.text = "Score: " + score;
        highScoreText.text = "High Score: " + highScore;
    }
}
