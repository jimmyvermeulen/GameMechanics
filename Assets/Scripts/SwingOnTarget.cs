using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwingOnTarget : MonoBehaviour {
    [SerializeField]
    PlayerController playerController;
    Rigidbody2D targetRB;
	// Use this for initialization
	void Start () {
        targetRB = GetComponent<Rigidbody2D>();
	}

    void Update()
    {
        
    }

    private void OnMouseDown()
    {
        Vector2 mousePosition = Camera.main.ScreenPointToRay(Input.mousePosition).origin;
        playerController.SwingFromTarget(targetRB);
    }
}
