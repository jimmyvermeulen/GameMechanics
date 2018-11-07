using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BossBehaviour : MonoBehaviour {
    public Sprite throwSprite;
    public Sprite leftSprite;
    public Sprite rightSprite;
    public Sprite hitSprite;

    public SpriteRenderer[] heartSprites;
    
    private Animation bossAnimation;

    public enum State { Throwing, Moving, Hurting}
    public State currentState = State.Throwing;
    [SerializeField] float health;
    Vector3 startPosition;
    public float hurtTime;
    float hurtPhase = 0;
    public float moveSpeed;
    private float throwCount;
    private float throwTimer = 0;
    public float throwDelay = 1f;
    public int moveTarget = 0;
    public float maxLeft;
    public float maxRight;
    private SpriteRenderer spriteRenderer;
    public GameObject throwObject;
    public float throwSpeed;
    public GameObject player;
    public GameObject bananasEnd;
    

	// Use this for initialization
	void Start () {
        spriteRenderer = GetComponent<SpriteRenderer>();
        startPosition = transform.position;
        throwTimer = Time.time + throwDelay;
        bossAnimation = GetComponent<Animation>();
    }
	
	// Update is called once per frame
	void Update () {
        switch (currentState)
        {
            case State.Throwing:
                spriteRenderer.sprite = throwSprite;
                if (throwTimer < Time.time)
                {
                    if (throwCount >= 3)
                    {
                        currentState = State.Moving;
                        throwCount = 0;
                        break;
                    }
                    //bossAnimation.Play("bossthrow");
                    Debug.Log(throwCount);
                    GameObject tempObject = Instantiate(throwObject);
                    Rigidbody2D tempRb = tempObject.GetComponent<Rigidbody2D>();
                    tempRb.velocity = Vector3.Normalize(player.transform.position - gameObject.transform.position) * throwSpeed;
                    tempRb.angularVelocity = 180;
                    throwCount++;
                    throwTimer = Time.time + throwDelay;
                }
                break;
            case State.Moving:
                Debug.Log(moveTarget);
                if(moveTarget == 0 || moveTarget == 2)
                {
                    spriteRenderer.sprite = leftSprite;
                    if (transform.position.x <= maxLeft) {
                        transform.position = new Vector2(maxLeft, startPosition.y);
                        moveTarget++;
                    }
                    else
                    {
                        transform.position -= new Vector3(moveSpeed * Time.deltaTime, 0);
                    }
                }
                if(moveTarget == 1 || moveTarget == 3)
                {
                    spriteRenderer.sprite = rightSprite;
                    if (transform.position.x >= maxRight)
                    {
                        transform.position = new Vector2(maxRight, transform.position.y);
                        moveTarget++;
                    }
                    else
                    {
                        transform.position += new Vector3(moveSpeed * Time.deltaTime, 0);
                    }
                }
                if(moveTarget == 4)
                {
                    spriteRenderer.sprite = leftSprite;
                    if (transform.position.x <= startPosition.x)
                    {
                        transform.position = new Vector2(startPosition.x, startPosition.y);
                        moveTarget = 0;
                        currentState = State.Throwing;
                        throwTimer = Time.time + throwDelay;
                    }
                    else
                    {
                        transform.position -= new Vector3(moveSpeed * Time.deltaTime, 0);
                    }
                }
                break;
            case State.Hurting:
                spriteRenderer.sprite = hitSprite;
                hurtPhase += Time.deltaTime / hurtTime;
                if (hurtPhase >= 1.0f)
                {
                    transform.position = startPosition;
                    currentState = State.Throwing;
                    hurtPhase = 1.0f;
                }
                transform.position = new Vector3(Mathf.Lerp(transform.position.x, startPosition.x, hurtPhase), transform.position.y);
                break;
        }
	}

    public void Hit()
    {
        throwSpeed *= 1.5f;
        throwDelay *= 0.8f;
        moveSpeed *= 1.2f;
        moveTarget = 0;

        currentState = State.Hurting;
        health--;
        if (health < 3)
            heartSprites[2].color = Color.black;
        if (health < 2)
            heartSprites[1].color = Color.black;
        if (health < 1)
            heartSprites[0].color = Color.black;
        if (health <= 0)
        {
            Death();
        }
        else
        {
            bossAnimation.Play("bosshit");
        }
    }

    public void Death()
    {
        bananasEnd.SetActive(true);
        Destroy(gameObject);
    }
}
