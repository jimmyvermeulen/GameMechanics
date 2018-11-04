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
    private float maxRopeLength = 0;
    [SerializeField]
    private float ropeDistanceMultiplier = 1;
    [SerializeField]
    private LayerMask groundLayer;
    [SerializeField]
    private MoveContinuously elephants;
    private SpriteRenderer renderer;
    public bool raging = true;
    private GameObject[] swingTargets;
    private GameObject target;
    private Rigidbody2D targetRb;

	// Use this for initialization
	void Start () {
        swingTargets = GameObject.FindGameObjectsWithTag("SwingTarget");
        playerRigidbody = GetComponent<Rigidbody2D>();
        playerCollider = GetComponent<Collider2D>();
        swingJoint = GetComponent<SpringJoint2D>();
        ropeLine = GetComponent<LineRenderer>();
        ropeLine.SetPosition(0, transform.position);
        renderer = GetComponent<SpriteRenderer>();
        if(SceneManager.GetActiveScene().buildIndex == 1)
        {
            targetingLine.startWidth = 0;
            targetingLine.endWidth = 0;
        }
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKey(KeyCode.A))
        {
            MovePlayer(false);
            renderer.flipX = true;
        }
        if (Input.GetKey(KeyCode.D))
        {
            MovePlayer(true);
            renderer.flipX = false;
        }
        if (Input.GetKeyDown(KeyCode.W) && isGrounded())
        {
            playerRigidbody.AddForce(new Vector2(0, jumpSpeed));
        }

        if (targetingLine.enabled)
        {
            /*Vector2 targetPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
            targetPosition.Normalize();
            targetPosition *= 1000;
            */
            float shortestDistance = float.MaxValue;
            target = null;
            targetRb = null;
            foreach (GameObject swingTarget in swingTargets)
            {
                float tempDistance = Vector2.Distance(swingTarget.transform.position, Camera.main.ScreenToWorldPoint(Input.mousePosition));
                if (tempDistance < shortestDistance)
                {
                    shortestDistance = tempDistance;
                    if (target != swingTarget)
                    {
                        target = swingTarget;
                    }
                }
            }

            targetingLine.SetPosition(0, transform.position);
            if (target != null)
            {
                Vector2 targetDirection = target.transform.position - transform.position;
                targetDirection.Normalize();
                int layerMask = 1 << 8;

                RaycastHit2D hit = Physics2D.Raycast(transform.position, targetDirection, maxRopeLength, layerMask);
                if (hit.collider != null && hit.transform.tag == "SwingTarget")
                {
                    targetingLine.SetPosition(1, target.transform.position);
                    targetRb = target.GetComponent<Rigidbody2D>();
                }
                else
                {
                    targetingLine.SetPosition(1, transform.position);
                    target = null;
                    targetRb = null;
                }
            }
        }

        if (Input.GetMouseButtonDown(0))
        {
            if(targetRb != null)
                SwingFromTarget(targetRb);
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

        //Debug.DrawRay(bottomLeft, Vector2.down * 0.1f, Color.green);
        RaycastHit2D hitLeft = Physics2D.Raycast(bottomLeft, Vector2.down, 0.1f, groundLayer);
        //Debug.DrawRay(bottomRight, Vector2.down * 0.1f, Color.green);
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
        if(collision.transform.tag == "Barrel" || collision.transform.tag == "BossMonkey")
        {
            GameObject[] barrels = GameObject.FindGameObjectsWithTag("Barrel");
            foreach(GameObject barrel in barrels)
            {
                Destroy(barrel);
            }
            Death();
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {


    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "Spider")
        {
            Death();
        }
    }

    private void Death()
    {
        swingJoint.enabled = false;
        ropeLine.enabled = false;
        if (GameManager.instance.playerHealth <= 0)
        {
            SceneManager.LoadScene("Game Over");
        }
        else
        {
            transform.position = startLocation.position;
            playerRigidbody.velocity = Vector2.zero;
            GameManager.instance.playerHealth--;
            if(elephants != null)
                elephants.Reset();
        }
    }
}
