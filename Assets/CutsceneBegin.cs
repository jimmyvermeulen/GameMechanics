using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CutsceneBegin : MonoBehaviour {
    public Animation monkey;
    public Animation girlMonkey;
    public Animation bossMonkey;
    public Animation cameraAnimation;
    public Animation bananas;
    public AudioClip eatSound;
    public AudioClip landingSound;
    public AudioClip monkeyLaugh;
    public AudioClip bossPickup;
    public GameObject[] handBananas;
    public GameObject heart;
    public GameObject bossMonkeyObject;
    public GameObject bananasObject;
    public GameObject girlMonkeyObject;
    public SpriteRenderer bossmonkeySpriteRenderer;
    public SpriteRenderer monkeySpriteRenderer;
    public Sprite pickupBossSprite;
    public Sprite pickupMonkeyGirlBossSprite;
    public Sprite sadMonkey;
    private AudioSource sounds;

    bool startedFall = false;
    bool pickingUpBananas = false;
    bool pickingUpGirlMonkey = false;
    bool pickedUpGirlMonkey = false;
    bool movingAway = false;
    bool movedAway = false;

	// Use this for initialization
	void Start () {
        sounds = GetComponent<AudioSource>();
        Invoke("monkeyEatBanana", 2.0f);
        Invoke("girlMonkeyEatBanana", 4.0f);
        Invoke("StartBossMonkeyFall", 6.0f);
	}
	
	// Update is called once per frame
	void Update () {
		if(!bossMonkey.isPlaying && startedFall)
        {
            startedFall = false;
            sounds.clip = landingSound;
            sounds.Play();
            monkey.Play();
            cameraAnimation.Play();
            bossMonkey.Play("pickupBananas");
            pickingUpBananas = true;
        }
        if(!bossMonkey.isPlaying && pickingUpBananas)
        {
            pickingUpBananas = false;
            bananas.Play();
            sounds.clip = bossPickup;
            sounds.Play();
            bossmonkeySpriteRenderer.sprite = pickupBossSprite;
            bossmonkeySpriteRenderer.flipY = true;
            pickingUpGirlMonkey = true;
        }
        if (!bananas.isPlaying && pickingUpGirlMonkey)
        {
            bananasObject.transform.parent = bossMonkeyObject.transform;
            bananasObject.transform.localPosition = Vector3.zero;
            pickingUpGirlMonkey = false;
            bossMonkey.Play("pickupGirlMonkey");
            pickedUpGirlMonkey = true;
        }
        if (!bossMonkey.isPlaying && pickedUpGirlMonkey)
        {
            pickedUpGirlMonkey = false;
            girlMonkey.Play("GirlMonkeyMoveToBoss");
            sounds.clip = bossPickup;
            sounds.Play();
            movingAway = true;
        }
        if(!girlMonkey.isPlaying && movingAway)
        {
            sounds.clip = monkeyLaugh;
            sounds.Play();
            bossmonkeySpriteRenderer.sprite = pickupMonkeyGirlBossSprite;
            bossmonkeySpriteRenderer.flipY = false;
            girlMonkeyObject.transform.parent = bossMonkeyObject.transform;
            girlMonkeyObject.transform.localPosition = new Vector3(-0.1f, 0.1f);
            movingAway = false;
            bossMonkey.Play("MovingAway");
            movedAway = true;
        }
        if(!bossMonkey.isPlaying && movedAway)
        {
            movedAway = false;
            monkeySpriteRenderer.sprite = sadMonkey;
            Invoke("StartGame", 3.0f);
        }
    }

    void StartBossMonkeyFall()
    {
        Destroy(heart);
        bossMonkey.Play();
        startedFall = true;
    }

    void monkeyEatBanana()
    {
        sounds.clip = eatSound;
        sounds.Play();
        Destroy(handBananas[0]);
    }


    void girlMonkeyEatBanana()
    {
        sounds.Play();
        Destroy(handBananas[1]);
    }

    void StartGame()
    {
        SceneManager.LoadScene(1);
    }
    
}
