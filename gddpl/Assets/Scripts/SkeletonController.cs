using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonController : MonoBehaviour
{
    private Animator animator;
    private Rigidbody2D rb;
    private float lastAttackedAt = -9999f;


    [Header("General")]
    [SerializeField]
    private float movementSpeed;  
    [SerializeField]
    private LayerMask obstacles;
    [SerializeField]
    private Transform scanPoint;
    private bool isDead;
    private float turnDistance = 1.5f;

    [Header("Attacks")]
    [SerializeField]
    private LayerMask targetLayers;
    [SerializeField]
    private LayerMask visibleLayers;
    [SerializeField]
    private float vision;
    [SerializeField]
    private float range;
    private Transform player;
    [SerializeField]
    private float attackCooldown;
    [SerializeField]
    private float attackSpeedSlow;
    [SerializeField]
    private float damageSlow;
    [SerializeField]
    private float attackSpeedFast;
    [SerializeField]
    private float damageFast;

    private void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        isDead = animator.GetBool("Dead");

        //set speed for attacks
        animator.SetFloat("attackSpeedA", attackSpeedSlow);
        animator.SetFloat("attackSpeedB", attackSpeedFast);
    }

    private void FixedUpdate()
    {        
        if (WallOrGapAhead()) ChangeDirection();
        if (PlayerVisible())
        {
            //Debug.Log("Dist: " + GetDistance());
            if (GetDistance() <= range)
            {
                if (Time.time > lastAttackedAt + attackCooldown)
                {
                    AttackB();
                    lastAttackedAt = Time.time;
                }
            }
        }
        Move();
    }

    private void AttackA()
    {
        Debug.Log("AttackA");
        rb.velocity = new Vector2(0, rb.velocity.y);
        animator.SetTrigger("attackA");
        
    }

    private void AttackB()
    {
        Debug.Log("AttackB");
        rb.velocity = new Vector2(0, rb.velocity.y);
        animator.SetTrigger("attackB");
    }

    private void Move()
    {
        //check if movement is allowed during animation
        if (isMoveAllowed()) {
        float horizontalVelocity = transform.right.x * movementSpeed * Time.fixedDeltaTime;
        rb.velocity = new Vector2(horizontalVelocity, rb.velocity.y);
        animator.SetFloat("horizontalSpeed", Mathf.Abs(horizontalVelocity)); }
    }

    private bool isMoveAllowed()
    {
        if (isDead) return false;
        if (GetDistance() <= range) return false;
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("skelly_AttackA")) return false;
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("skelly_AttackB")) return false;
        return true;
    }
    private void ChangeDirection()
    {
        if (transform.eulerAngles == Vector3.zero) transform.eulerAngles = new Vector3(0.0f, 180.0f, 0.0f);
        else transform.eulerAngles = Vector3.zero;
    }

    private bool WallOrGapAhead()
    {
        RaycastHit2D wallHit = Physics2D.Raycast(scanPoint.position, transform.right, turnDistance, obstacles);
        RaycastHit2D floorHit = Physics2D.Raycast(scanPoint.position, -transform.up, scanPoint.localPosition.y + 1.0f, obstacles);
        return floorHit.collider == null || wallHit.collider != null;
    }

    private bool PlayerVisible()
    {
        bool playerHit = false;
        RaycastHit2D hit = Physics2D.Raycast(scanPoint.position, transform.right, vision, visibleLayers);
        if (hit.collider != null)
        {
            if ((targetLayers.value & (1 << hit.collider.gameObject.layer)) > 0)
            {
                playerHit = true;
            }
        }
        return playerHit;
    }

    private float GetDistance()
    {
        return Vector2.Distance(this.transform.position, player.position);
    }

}
