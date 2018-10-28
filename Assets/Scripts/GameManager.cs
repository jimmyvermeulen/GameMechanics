using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {
    [SerializeField]
    private float _playerHealth = 0;
    private float _bananas = 0;
    private float _collectibles = 0;
    [SerializeField]
    private float _maxCollectibles = 0;
    [SerializeField]
    private Text healthText, bananasText, collectiblesText;
    public static GameManager instance = null;

	// Use this for initialization
	void Start () {
        DontDestroyOnLoad(gameObject);
        instance = this;
        playerHealth = playerHealth;
        bananas = bananas;
        collectibles = collectibles;
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public float playerHealth
    {
        get { return _playerHealth; }
        set
        {
            _playerHealth = value;
            healthText.text = "Health: " + _playerHealth;
        }
    }

    public float bananas
    {
        get { return _bananas; }
        set
        {
            _bananas = value;
            bananasText.text = "Bananas: " + _bananas;

        }
    }

    public float collectibles
    {
        get { return _collectibles; }
        set
        {
            _collectibles = value;
            collectiblesText.text = "Collectibles: " + _collectibles;
        }
    }

    public void DisableGameUI()
    {
        healthText.enabled = false;
        bananasText.enabled = false;
        collectiblesText.enabled = false;
    }
}
