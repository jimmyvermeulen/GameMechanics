using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cutsceneLevel3 : MonoBehaviour {
    public Animation bossMonkeyAnimation;
    public Animation girlMonkeyAnimation;
    public Animation bananasAnimation;
    public Camera playerCamera;
    public Transform cameraTargetPosition;
    public PlayerController player;

    public SpriteRenderer bossSpriteRenderer;
    public SpriteRenderer playerSpriteRenderer;
    public Sprite angryBossSprite;
    public Sprite sadMonkeySprite;
    public Sprite angryMonkeySprite;
    public AudioSource girlMonkeySound;
    public AudioSource bossSound;
    public GameObject blood;
    public GameObject boss;
    public GameObject bananas;

    bool panningDone = false;
    bool angryPlaying = false;
    bool pickupBananas = false;
    bool panCameraBack = false;

    Vector3 cameraStartPosition;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
        if (angryPlaying && !bossMonkeyAnimation.isPlaying)
        {
            angryPlaying = false;
            bossMonkeyAnimation.Play("SquishMonkey");
            Invoke("SquishGirlMonkey", 1.2f);
            pickupBananas = true;
        }
        if (pickupBananas && !bossMonkeyAnimation.isPlaying)
        {
            pickupBananas = false;
            bossMonkeyAnimation.Play("PickupBananas");
            Invoke("bananasToBoss", 1.0f);
            panCameraBack = true;
        }
        if(panCameraBack && !bossMonkeyAnimation.isPlaying)
        {
            panCameraBack = false;
            StartCoroutine(PanCamera(cameraTargetPosition.position, cameraStartPosition, false));
        }
	}

    public void StartScene()
    {
        Debug.Log("cutscene start");
        GameManager.instance.StopTimeBonus();
        player.paused = true;
        cameraStartPosition = playerCamera.transform.position;
        StartCoroutine(PanCamera(cameraStartPosition, cameraTargetPosition.position, true));
    }

    public void AngryBoss()
    {
        bossSpriteRenderer.flipY = false;
        bossSpriteRenderer.sprite = angryBossSprite;
        bossMonkeyAnimation.Play("Boss Angry");
        bossSound.Play();
        angryPlaying = true;
    }

    public void SquishGirlMonkey()
    {
        girlMonkeyAnimation.Play();
        girlMonkeySound.Play();
        blood.SetActive(true);
        playerSpriteRenderer.sprite = sadMonkeySprite;
    }

    public void bananasToBoss()
    {
        bananasAnimation.Play();
    }

    public void EndCutScene()
    {
        Destroy(boss);
        Destroy(bananas);
        playerSpriteRenderer.sprite = angryMonkeySprite;
        playerSpriteRenderer.color = new Color(255, 160, 160);
        player.paused = false;
    }

    /*public void PanCamera()
    {
        playerCamera.posi
    }*/

    IEnumerator PanCamera(Vector3 fromPosition, Vector3 targetPosition, bool start)
    {
        for (float i = 0; i <= 10f; i += 0.1f)
        {
            Vector3 cameraPosition = Vector2.MoveTowards(fromPosition, targetPosition, i);
            cameraPosition.z = cameraStartPosition.z;
            playerCamera.transform.position = cameraPosition;
            yield return null;
        }
        if (start)
            AngryBoss();
        else
            EndCutScene();
        yield return null;
    }

}
