using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour {
    Rigidbody2D playerRigidbody;
    Collider2D playerCollider;
    SpringJoint2D swingJoint;
    LineRenderer ropeLine;
    public LineRenderer targetingLine;
    bool isSwinging = false;
    [SerializeField]
    private Transform startLocation;
    [SerializeField]
    private float horizontalSpeed = 0;
    [SerializeField]
    private float maxSpeed = 0;
    [SerializeField]
    private float jumpSpeed = 0;
    [SerializeField]
    private float ropeDistanceMultiplier = 1;
    [SerializeField]
    private LayerMask groundLayer;

	// Use this for initialization
	void Start () {
        playerRigidbody = GetComponent<Rigidbody2D>();
        playerCollider = GetComponent<Collider2D>();
        swingJoint = GetComponent<SpringJoint2D>();
        ropeLine = GetComponent<LineRenderer>();
        ropeLine.SetPosition(0, transform.position);
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKey(KeyCode.A))
        {
            MovePlayer(false);
        }
        if (Input.GetKey(KeyCode.D))
        {
            MovePlayer(true);
        }
        if (Input.GetKeyDown(KeyCode.W) && isGrounded())
        {
            playerRigidbody.AddForce(new Vector2(0, jumpSpeed));
        }

        if (Input.GetMouseButtonDown(0))
        {
            Vector2 targetDirection = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
            targetDirection.Normalize();

            RaycastHit2D hit = Physics2D.Raycast(transform.position, targetDirection, 1000f);
            if (hit.collider != null && hit.transform.tag == "SwingTarget")
            {
                SwingFromTarget(hit.rigidbody);
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            swingJoint.enabled = false;
            ropeLine.enabled = false;
            targetingLine.enabled = true;
            isSwinging = false;
        }

        if (ropeLine.enabled)
        {
            ropeLine.SetPosition(0, transform.position);
        }

        if (targetingLine.enabled)
        {
            Vector2 targetPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
            targetPosition.Normalize();
            targetPosition *= 1000;
            targetingLine.SetPosition(0, transform.position);
            targetingLine.SetPosition(1, targetPosition);
        }
    }

    public void SwingFromTarget(Rigidbody2D target)
    {
        swingJoint.enabled = true;
        targetingLine.enabled = false;
        swingJoint.connectedBody = target;
        swingJoint.distance = Vector2.Distance(transform.position, swingJoint.connectedBody.transform.position) * ropeDistanceMultiplier;
        ropeLine.enabled = true;
        ropeLine.SetPosition(1, swingJoint.connectedBody.transform.position);
        isSwinging = true;
    }

    public void MovePlayer(bool MoveForward)
    {
        float tempSpeed = horizontalSpeed;
        float tempMaxSpeed = maxSpeed;
        if (!isGrounded() || isSwinging)
        {
            tempSpeed /= 4;
            tempMaxSpeed /= 4;
        }
        if (MoveForward)
        {
            if (playerRigidbody.velocity.x < tempMaxSpeed)
                playerRigidbody.AddForce(new Vector2(tempSpeed, 0));
        }
        else
        {
            if (playerRigidbody.velocity.x > -tempMaxSpeed)
                playerRigidbody.AddForce(new Vector2(-tempSpeed, 0));
        }
    }

    private bool isGrounded()
    {
        Vector2 bottomLeft = playerCollider.bounds.center;
        bottomLeft.x -= playerCollider.bounds.extents.x;
        bottomLeft.y -= playerCollider.bounds.extents.y;

        Vector2 bottomRight = playerCollider.bounds.center;
        bottomRight.x += playerCollider.bounds.extents.x;
        bottomRight.y -= playerCollider.bounds.extents.y;

        Debug.DrawRay(bottomLeft, Vector2.down * 0.1f, Color.green);
        RaycastHit2D hitLeft = Physics2D.Raycast(bottomLeft, Vector2.down, 0.1f, groundLayer);
        Debug.DrawRay(bottomRight, Vector2.down * 0.1f, Color.green);
        RaycastHit2D hitRight = Physics2D.Raycast(bottomRight, Vector2.down, 0.1f, groundLayer);

        if (hitLeft || hitRight)
        {
            return true;
        }
        else
        {
            return false;
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.transform.tag == "FallArea")
        {
            Death();
        }
    }

    private void Death()
    {
        if (GameManager.instance.playerHealth <= 0)
        {
            SceneManager.LoadScene("Game Over");
        }
        else
        {
            transform.position = startLocation.position;
            playerRigidbody.velocity = Vector2.zero;
            GameManager.instance.playerHealth--;
        }
    }
}
