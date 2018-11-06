using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmoothFollowPlayer : MonoBehaviour {
    public Transform player;
    public float followSpeed;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        Vector2 direction = Vector3.Normalize(player.position - transform.position);
        float distance = Vector2.Distance(player.position, transform.position);
        transform.position += (Vector3)direction * distance * followSpeed * Time.deltaTime;
	}
}
