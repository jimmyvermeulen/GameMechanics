using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BananaPickup : MonoBehaviour {
    private AudioSource bananaSound;

    private void Start()
    {
        bananaSound = GetComponent<AudioSource>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.transform.tag == "Player")
        {
            GameManager.instance.bananas++;
            GetComponent<Collider2D>().enabled = false;
            GetComponent<SpriteRenderer>().enabled = false;
            bananaSound.Play();
        }
    }
}
