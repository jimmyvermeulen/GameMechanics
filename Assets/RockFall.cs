using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockFall : MonoBehaviour {
    Rigidbody2D rockRb;
    public float throwSpeed;
    public Transform bossMonkey;
    bool thrown = false;

	// Use this for initialization
	void Start () {
        rockRb = GetComponent<Rigidbody2D>();
    }
	
	// Update is called once per frame
	void Update () {
        if (thrown)
        {
            rockRb.velocity = Vector3.Normalize(bossMonkey.transform.position - gameObject.transform.position) * throwSpeed;
        }
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            if (collision.GetComponent<PlayerController>().raging)
                thrown = true;
        }
        if(collision.tag == "BossMonkey")
        {
            collision.GetComponent<BossBehaviour>().Hit();
            Destroy(this);
        }
    }
}
