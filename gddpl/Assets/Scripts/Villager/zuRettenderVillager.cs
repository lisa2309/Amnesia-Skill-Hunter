using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class zuRettenderVillager : MonoBehaviour
{
    //cached references
    private Rigidbody2D rb;
    private Animator animator;


    private float lastMoved = -9999;

    [SerializeField]
    private float animationCooldown = 10.0f;

    [SerializeField]
    private float moveSpeed = 200.0f;

    [SerializeField]
    private GameObject player;

    [SerializeField]
    private float vision = 5.0f;

    [SerializeField]
    private LayerMask visibleLayers;

    [SerializeField]
    private Transform scanPoint;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(isFree())Move();
    }

    private void Move()
    {
        float horizontalVelocity = transform.right.x * moveSpeed * Time.fixedDeltaTime;
        rb.velocity = new Vector2(horizontalVelocity, rb.velocity.y);

        //animation
        animator.SetFloat("HorizontalSpeed", Mathf.Abs(horizontalVelocity));
    }

    private bool isFree()
    {
        bool isFree = false;
        RaycastHit2D hit = Physics2D.Raycast(scanPoint.position, transform.right, vision, visibleLayers);
        if (hit.collider != null)
        {
            isFree = true;
        }
        return isFree;
    }
}
