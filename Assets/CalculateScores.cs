using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CalculateScores : MonoBehaviour {
    [SerializeField]
    private Text bananaText, collectibleText, scoreText;
    [SerializeField]
    private float bananaScore, collectibleScore;

	// Use this for initialization
	void Start () {
        GameManager.instance.DisableGameUI();
        calculateScoreTexts();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void calculateScoreTexts()
    {
        bananaText.text = "Bananas: " + GameManager.instance.bananas + " X " + bananaScore + " = " + (bananaScore * GameManager.instance.bananas);
        //collectibleText.text = "Collectibles: " + GameManager.instance.collectibles + " X " + collectibleScore + " = " + (collectibleScore * GameManager.instance.collectibles);
        scoreText.text = "" + (/*(GameManager.instance.collectibles * collectibleScore)*/ + (GameManager.instance.bananas * bananaScore));
    }
}
