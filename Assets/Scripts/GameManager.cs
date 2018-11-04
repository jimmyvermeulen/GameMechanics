using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {
    [SerializeField]
    private float _playerHealth = 0;
    private float _bananas = 0;
    private float _adrenaline = 0;
    [SerializeField]
    public Text healthText, bananasText, adrenalineText;
    public RectTransform adrenalineBar;
    public float adrenalineBarWidth;
    public static GameManager instance = null;
    private int _level = 1;

	// Use this for initialization
	void Start () {
        DontDestroyOnLoad(gameObject);
        instance = this;
        playerHealth = playerHealth;
        bananas = bananas;
        adrenaline = adrenaline;
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

    public float adrenaline
    {
        get { return _adrenaline; }
        set
        {
            _adrenaline = value;
            //adrenalineText.text = "Adrenaline: " + _adrenaline;
            adrenalineBar.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, _adrenaline / 100 * adrenalineBarWidth);
        }
    }

    public void DisableGameUI()
    {
        healthText.enabled = false;
        bananasText.enabled = false;
        adrenalineText.enabled = false;
    }

    public int level
    {
        get { return _level; }
        set
        {
            _level= value;
        }
    }
}
