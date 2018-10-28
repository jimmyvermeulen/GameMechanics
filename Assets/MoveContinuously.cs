using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveContinuously : MonoBehaviour {
    [SerializeField]
    Vector2 moveSpeed;
    Vector2 startPosition;

	// Use this for initialization
	void Awake () {
        startPosition = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
        transform.position += (Vector3)moveSpeed * Time.deltaTime;
	}

    public void Reset()
    {
        transform.position = startPosition;
    }
}
