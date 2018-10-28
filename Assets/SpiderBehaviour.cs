using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderBehaviour : MonoBehaviour {

    [SerializeField]
    private LayerMask groundLayer;
    [SerializeField]
    private float moveSpeed;
    private Collider2D spiderCollider;
    private SpriteRenderer spriteRenderer;
    float moveDirection = 1;

    // Use this for initialization
    void Start () {
        spiderCollider = GetComponent<Collider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
        checkEdge();
        transform.position += new Vector3(moveSpeed * Time.deltaTime * moveDirection, 0);
	}

    void checkEdge()
    {
        Vector2 bottomLeft = spiderCollider.bounds.center;
        bottomLeft.x -= spiderCollider.bounds.extents.x;
        bottomLeft.y -= spiderCollider.bounds.extents.y;

        Vector2 bottomRight = spiderCollider.bounds.center;
        bottomRight.x += spiderCollider.bounds.extents.x;
        bottomRight.y -= spiderCollider.bounds.extents.y;

        //Debug.DrawRay(bottomLeft, Vector2.down * 0.1f, Color.green);
        RaycastHit2D hitLeft = Physics2D.Raycast(bottomLeft, Vector2.down, 0.1f, groundLayer);
        //Debug.DrawRay(bottomRight, Vector2.down * 0.1f, Color.green);
        RaycastHit2D hitRight = Physics2D.Raycast(bottomRight, Vector2.down, 0.1f, groundLayer);

        if((moveDirection == -1 && hitLeft == false) || (moveDirection == 1 && hitRight == false))
        {
            changeDirection();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.transform.tag == "Wall")
            changeDirection();
    }

    void changeDirection()
    {
        spriteRenderer.flipX = !spriteRenderer.flipX;
        moveDirection *= -1;
    }
}
