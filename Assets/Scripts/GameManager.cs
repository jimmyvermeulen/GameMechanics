using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {
    [SerializeField]
    private int _playerHealth = 0;
    private int _bananas = 0;
    private int _timeBonus = 0;
    [SerializeField]
    public Text healthText, bananasText, timeBonusText;
    public Image healthImage, bananasImage, hudBackgroundImage;
    public float adrenalineBarWidth;
    public static GameManager instance = null;
    private int _level = 1;
    private AudioSource backgroundMusic;
    public AudioClip normalBackgroundMusic;
    public AudioClip level3Music;
    public AudioClip gameOverBackgroundMusic;
    public AudioClip bossBackgroundMusic;
    public AudioClip victoryMusic;
    public int[] levelTimeBonuses;

	// Use this for initialization
	void Start () {
        SceneManager.sceneLoaded += OnLevelLoaded;
        DontDestroyOnLoad(gameObject);
        instance = this;
        playerHealth = playerHealth;
        bananas = bananas;
        timeBonus = timeBonus;
        backgroundMusic = GetComponent<AudioSource>();
        PlayBackgroundMusic();
        StartTimeBonus();
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded += OnLevelLoaded;
    }

    // Update is called once per frame
    void Update () {
		
	}

    public int playerHealth
    {
        get { return _playerHealth; }
        set
        {
            _playerHealth = value;
            healthText.text = " X " + _playerHealth;
        }
    }

    public int bananas
    {
        get { return _bananas; }
        set
        {
            _bananas = value;
            bananasText.text = " X " + _bananas;

        }
    }

    public void DisableGameUI()
    {
        healthText.enabled = false;
        bananasText.enabled = false;
        timeBonusText.enabled = false;
    }

    public int level
    {
        get { return _level; }
        set
        {
            _level= value;
        }
    }

    public int timeBonus
    {
        get { return _timeBonus; }
        set
        {
            _timeBonus = value;
            timeBonusText.text = "Time Bonus: " + _timeBonus;
        }
    }

    private void OnLevelLoaded(Scene scene, LoadSceneMode mode)
    {
        PlayBackgroundMusic();
        StartTimeBonus();
    }

    private void StartTimeBonus()
    {
        Debug.Log("starttimebonus");
        if (level <= levelTimeBonuses.Length)
        {
            timeBonus = levelTimeBonuses[level-1];
            CancelInvoke("UpdateTimeBonus");
            InvokeRepeating("UpdateTimeBonus", 1f, 1f);
        }
    }

    void UpdateTimeBonus()
    {
        timeBonus -= 20;
    }

    public void StopTimeBonus()
    {
        CancelInvoke("UpdateTimeBonus");
    }

    private void PlayBackgroundMusic()
    {
        if (level == 1 && level == 2 && backgroundMusic.clip != normalBackgroundMusic)
        {
            backgroundMusic.Stop();
            backgroundMusic.clip = normalBackgroundMusic;
            backgroundMusic.volume = 0.3f;
            backgroundMusic.Play();
        }
        else if (level == 3 && backgroundMusic.clip != level3Music)
        {
            backgroundMusic.Stop();
            backgroundMusic.clip = level3Music;
            backgroundMusic.volume = 0.3f;
            backgroundMusic.Play();
        }
        else if (level == 4 && backgroundMusic.clip != bossBackgroundMusic)
        {
            backgroundMusic.Stop();
            backgroundMusic.clip = bossBackgroundMusic;
            backgroundMusic.volume = 0.3f;
            backgroundMusic.Play();
        }
        else if (level == 5 && backgroundMusic.clip != gameOverBackgroundMusic)
        {
            backgroundMusic.Stop();
            backgroundMusic.clip = gameOverBackgroundMusic;
            backgroundMusic.volume = 0.3f;
            backgroundMusic.Play();
        }
        else if (level == 6 && backgroundMusic.clip != victoryMusic)
        {
            backgroundMusic.Stop();
            backgroundMusic.clip = victoryMusic;
            backgroundMusic.volume = 0.3f;
            backgroundMusic.Play();
        }
    }
}
