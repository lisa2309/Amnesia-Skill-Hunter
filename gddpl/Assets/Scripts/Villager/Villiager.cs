using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Villiager : MonoBehaviour
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
        Physics2D.IgnoreLayerCollision(7, 7);
    }

    // Update is called once per frame
    void FixedUpdate()
    {

            if (Time.time > lastMoved + animationCooldown)
            {
                animator.SetTrigger("Chillax");
                Debug.Log("Player visible");
            lastMoved = Time.time;
            }
        
    }



   
}
