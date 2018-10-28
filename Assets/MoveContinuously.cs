using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveContinuously : MonoBehaviour {
    [SerializeField]
    Vector2 moveSpeed;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        transform.position += (Vector3)moveSpeed * Time.deltaTime;
	}
}
