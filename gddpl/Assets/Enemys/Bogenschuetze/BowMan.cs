using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BowMan : MonoBehaviour
{
    [SerializeField]
    private Transform playerDetector;
    [SerializeField]
    private Transform shootpoint;

    [Header("Movement")]
    [SerializeField]
    private float movementSpeed;
    [SerializeField]
    private float turnDistance;
    [SerializeField]
    private LayerMask obstacles;
    [SerializeField]
    private Transform scanPoint;

    [SerializeField]
    private float attackRange = 4;
    [SerializeField]
    private int damage;
    [SerializeField]
    private float shootingSpeed;
    [SerializeField]
    private LayerMask playerLayer;

    private bool shooting;
    private Vector3 shootingDirection;


    private Animator animator;
    private Rigidbody2D rb;
    private EnemyHealth health;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        health = GetComponent<EnemyHealth>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        if (WallOrGapAhead())
            {
            ChangeDirection();
            }

        /*
        //Flip();
        var playerPosition = IsPlayerInRange();
        if (playerPosition != Vector3.zero)
        {
            ChangeDirection();
        }
        */
        
    }


    private void Flip()
    {
        if (rb.velocity.x > 0.0f) transform.eulerAngles = Vector3.zero;
        else if (rb.velocity.x < 0.0f) transform.eulerAngles = new Vector3(0.0f, 180.0f, 0.0f);
    }

    private void ChangeDirection()
    {
        if (transform.eulerAngles == Vector3.zero) transform.eulerAngles = new Vector3(0.0f, 180.0f, 0.0f);
        else transform.eulerAngles = Vector3.zero;
    }

    private Vector3 IsPlayerInRange()
    {
        var player = Physics2D.OverlapCircleAll(playerDetector.position, attackRange, playerLayer);
        if(player.Length != 0)
        {
            return player[0].transform.position;
        }

        return Vector3.zero;
    }

    private void Move()
    {
        float horizontalVelocity = transform.right.x * movementSpeed * Time.fixedDeltaTime;
        rb.velocity = new Vector2(horizontalVelocity, rb.velocity.y);
        animator.SetFloat("horizontalSpeed", Mathf.Abs(horizontalVelocity));
    }

    private bool WallOrGapAhead()
    {
        RaycastHit2D wallHit = Physics2D.Raycast(scanPoint.position, transform.right, turnDistance, obstacles);
        RaycastHit2D floorHit = Physics2D.Raycast(scanPoint.position, -transform.up, scanPoint.localPosition.y + 1.0f, obstacles);
        return floorHit.collider == null || wallHit.collider != null;
    }
}
