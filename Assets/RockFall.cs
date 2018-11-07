using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockFall : MonoBehaviour {
    Rigidbody2D rockRb;
    public float throwSpeed;
    Transform bossMonkey;
    Transform target;
    public RockSpawner rockSpawner;
    public cutsceneLevel3 cutscene;

    public string targetTag;
    bool thrown = false;

	// Use this for initialization
	void Start () {
        GameObject bossObject = GameObject.FindGameObjectWithTag("BossMonkey");
        if(bossObject != null)
            bossMonkey = bossObject.transform;
        rockRb = GetComponent<Rigidbody2D>();
        if(bossMonkey == null)
            target = GameObject.FindGameObjectWithTag(targetTag).transform;
    }
	
	// Update is called once per frame
	void Update () {
        if (thrown && bossMonkey != null)
        {
            rockRb.velocity = Vector3.Normalize(bossMonkey.transform.position - gameObject.transform.position) * throwSpeed;
        }
        else if (thrown)
        {
            rockRb.velocity = Vector3.Normalize(target.transform.position - gameObject.transform.position) * throwSpeed;
        }
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            if (collision.GetComponent<PlayerController>().raging)
                thrown = true;
        }
        if (thrown)
        {
            if (collision.tag == "BossMonkey")
            {
                BossBehaviour bossBehaviour = collision.GetComponent<BossBehaviour>();
                if (bossBehaviour != null)
                {
                    bossBehaviour.Hit();
                }
                else
                {
                    collision.gameObject.GetComponent<Animation>().Play();
                    cutscene.StartScene();
                }
                    
                if (rockSpawner != null)
                    rockSpawner.SpawnRock();
                Destroy(gameObject);
            }
            if (collision.tag == "FemaleMonkey")
            {
                //collision.GetComponent<FemaleMon>
            }
        }
    }

    private void OnDestroy()
    {
        
    }
}
