using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartLevel : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void Restart()
    {
        GameManager.instance.bananas = 0;
        GameManager.instance.collectibles = 0;
        SceneManager.LoadScene(GameManager.instance.level);
    }
}
