using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class VictoryScores : MonoBehaviour {
    [SerializeField]
    private TextMeshProUGUI previousScoreText, bananaText, livesText, timeBonusText, scoreText;
    private float bananaScore, liveScore, totalScore;
    public bool gameOver = false;

	// Use this for initialization
	void Start () {
        GameManager.instance.DisableGameUI();
        calculateScoreTexts();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void calculateScoreTexts()
    {
        if (gameOver)
        {
            previousScoreText.text = "Previous score: " + GameManager.instance.previousScore;
            bananaScore = GameManager.instance.bananaScore * GameManager.instance.bananas;
            bananaText.text = "Bananas: " + GameManager.instance.bananas + " X " + GameManager.instance.bananaScore + " = " + bananaScore;
            totalScore = GameManager.instance.previousScore + bananaScore;
            scoreText.text = "" + totalScore;
            GameManager.instance.previousScore = totalScore;
            GameManager.instance.playerHealth = 3;
        }
        else
        {
            previousScoreText.text = "Previous score: " + GameManager.instance.previousScore;
            bananaScore = GameManager.instance.bananaScore * GameManager.instance.bananas;
            bananaText.text = "Bananas: " + GameManager.instance.bananas + " X " + GameManager.instance.bananaScore + " = " + bananaScore;
            liveScore = GameManager.instance.livesScore * GameManager.instance.playerHealth;
            livesText.text = "Lives: " + GameManager.instance.playerHealth + " X " + GameManager.instance.livesScore + " = " + liveScore;
            timeBonusText.text = "Time Bonus: " + GameManager.instance.timeBonus;
            totalScore = GameManager.instance.timeBonus + GameManager.instance.previousScore + bananaScore + liveScore;
            scoreText.text = "" + totalScore;
            GameManager.instance.previousScore = totalScore;
            GameManager.instance.playerHealth = 3;
        }
    }
}
