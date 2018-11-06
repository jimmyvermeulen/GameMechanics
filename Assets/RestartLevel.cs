using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class RestartLevel : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Restart()
    {
        GameManager.instance.previousScore = 0;
        GameManager.instance.playerHealth = GameManager.instance.maxHealth;
        SceneManager.LoadScene(GameManager.instance.level);
        GameManager.instance.EnableGameUI();
    }
}
