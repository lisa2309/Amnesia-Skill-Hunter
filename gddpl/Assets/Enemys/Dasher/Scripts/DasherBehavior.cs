using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DasherBehavior : MonoBehaviour
{
    [SerializeField]
    private Transform playerDetector;
    [SerializeField]
    private Transform attackPoint;
    [SerializeField]
    private float attackRange;
    [SerializeField]
    private float dashSpeed = 400f;
    [SerializeField]
    private LayerMask playerLayers;
    [SerializeField]
    private float startDashTime;
    [SerializeField]
    private float attackTimeOutStart;
    [SerializeField]
    private float startAttackPrepTime;

    private float dashTime;
    private float attackPrepTime;
    private Vector3 dashDirection;
    private bool dashing;
    private Rigidbody2D rb;
    private float attackTimeOut;
    private Animator animator;
    private float hitTimeOut;
    private float startHitTimeOut = 1f;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        hitTimeOut = 0;
    }

    // Update is called once per frame
    void Update()
    {
        Flip();

        if (hitTimeOut >= 0)
            hitTimeOut -= Time.deltaTime;

        if (attackTimeOut >= 0)
            attackTimeOut -= Time.deltaTime;

        var playerPosition = IsPlayerInRange();
        if (playerPosition != Vector3.zero && attackTimeOut <= 0)
            startDash(playerPosition);

        if (dashing)
        {
            Physics2D.IgnoreLayerCollision(6, 7, true);
            attackTimeOut = attackTimeOutStart;
            Attack();
        }
        else
        {
            StopDash();
        }
        
        animator.SetBool("Dashing", dashing);
    }

    private void Attack()
    {
        if (dashTime <= 0)
        {
            dashDirection = Vector3.zero;
            dashing = false;
        }
        else
        {
            dashTime -= Time.deltaTime;
            rb.velocity = dashDirection * dashSpeed;
            if(hitTimeOut <= 0)
                DealDamage();
        }
    }

    private void startDash(Vector3 playerPosition)
    {
        dashing = true;
        dashTime = startDashTime;
        dashDirection = (playerPosition - this.transform.position).normalized;

    }

    private void StopDash()
    {
        rb.velocity = Vector2.zero;
        Physics2D.IgnoreLayerCollision(6, 7, false);
    }

    private Vector3 IsPlayerInRange()
    {
        var players = Physics2D.OverlapCircleAll(playerDetector.position, attackRange, playerLayers);
        if (players.Length != 0)
            return players[0].transform.position;
        
        return Vector3.zero;
    }

    private void Flip()
    {
        if (rb.velocity.x > 0.0f) transform.eulerAngles = Vector3.zero;
        else if (rb.velocity.x < 0.0f) transform.eulerAngles = new Vector3(0.0f, 180.0f, 0.0f);
    }

    private void DealDamage()
    {
        var hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, 0.1f, playerLayers);
        if (hitEnemies.Length != 0 && hitTimeOut <= 0)
        {
            hitEnemies[0].GetComponent<PlayerHealth>().LooseHealth(1);
            hitTimeOut = startHitTimeOut;
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (playerDetector == null)
            return;

        Gizmos.DrawWireSphere(playerDetector.position, attackRange);
    }
}
