using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

public class PlayerMovement : MonoBehaviour
{
    //cached references
    private Controls controls;
    private Rigidbody2D rb;
    private Animator animator;


    //state
    private float horizontalInput;
    private bool grounded;
    private float earlyJumpTimer;
    private float rememberGroundedTimer;
    private float initialJumpVelocity;
    private float maxJumpDuration;
    private float jumpTimer;
    private bool lowJump = false;
    private float runSpeedModifier = 1.0f;

    //config
    [Header("Run Parameters")]
    [SerializeField]
    private float runSpeed = 200.0f;

    [Header("Jump Parameters")]
    [SerializeField]
    private float jumpHeight = 100.0f;
    [SerializeField]
    private float lowJumpGravityScale = 2.0f;
    [SerializeField]
    private float fallingGravityScale = 3.0f;
    [SerializeField]
    private BoxCollider2D feetCollider;
    [SerializeField]
    private LayerMask walkableLayers;
    [SerializeField]
    private float timeToRememberEarlyJump = 0.2f;
    [SerializeField]
    private float timeToRememberGrounded = 0.125f;
    [SerializeField] [Range(0.0f,  1.0f)]
    private float relativeMinJumpDuaration = 0.33f;

    [Header("References")]
    [SerializeField]
    private float initialJumpSpeed = 6.0f;

    private void Awake()
    {
        controls = new Controls();

        controls.Gameplay.Run.performed += context => horizontalInput = context.ReadValue<float>();
        controls.Gameplay.Run.canceled += context => horizontalInput = 0.0f;

    }

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        CalculateJumpParameters();
    }

    private void FixedUpdate()
    {
        CheckGrounded();
        ApplyFallingGravityScale();
        UpdateTimers();

        Run();
        if (earlyJumpTimer > 0 && rememberGroundedTimer > 0.0f)
        {
            Jump();
        }

        Flip();
    }

    private void Run()
    {
        float horizontalVelocity = horizontalInput * runSpeed * runSpeedModifier * Time.fixedDeltaTime;
        rb.velocity = new Vector2(horizontalVelocity, rb.velocity.y);

        //animation
        //animator.SetFloat("RunSpeed", Mathf.Abs(horizontalVelocity));
    }

    public void SetRunSpeedModifier(float modifier)
    {
        runSpeedModifier = modifier;
    }

    public void ResetRunspeedModifier()
    {
        runSpeedModifier = 1.0f;
    }


    private void Flip()
    {
        if (rb.velocity.x > 0.0f)
        {
            transform.eulerAngles = Vector3.zero;
        }
        else if (rb.velocity.x < 0.0f)
        {
            transform.eulerAngles = new Vector3(0.0f, 180.0f, 0.0f);
        }
    }
    private void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, initialJumpVelocity);
        jumpTimer = maxJumpDuration;
        

        earlyJumpTimer = 0.0f;
        rememberGroundedTimer = 0.0f;

        //animations
        //animator.SetTrigger("Jump");
    }

    private void CancleJump()
    {
        if (jumpTimer > 0.0f)
        {
            lowJump = true;
        }
    }

    private void CheckGrounded()
    {
        grounded = feetCollider.IsTouchingLayers(walkableLayers);
        if (grounded)
        {
            rememberGroundedTimer = timeToRememberGrounded;
            rb.gravityScale = 1.0f;

            //animations
            //animator.SetBool("Falling", false);
        }
        //animator.SetBool("Grounded", grounded);
    }

    private void ApplyFallingGravityScale()
    {
        if (rb.velocity.y < 0.0f && !grounded)
        {
            rb.gravityScale = fallingGravityScale;

            //animations
            //animator.SetBool("Falling", true);
        }
    }

    private void SetEarlyJumpTimer()
    {
        earlyJumpTimer = timeToRememberEarlyJump;
    }

    private void CalculateJumpParameters()
    {
        maxJumpDuration = Mathf.Sqrt(-2.0f * jumpHeight / Physics2D.gravity.y);
        initialJumpVelocity = 2.0f * jumpHeight / maxJumpDuration;
    }

    private void UpdateTimers()
    {
        if (earlyJumpTimer > 0.0f)
        {
            earlyJumpTimer -= Time.fixedDeltaTime;
        }
        if (rememberGroundedTimer > 0.0f)
        {
            rememberGroundedTimer -= Time.fixedDeltaTime;
        }
        if (jumpTimer > 0.0f)
        {
            jumpTimer -= Time.fixedDeltaTime;
            if (lowJump && jumpTimer <= maxJumpDuration * (1.0f - relativeMinJumpDuaration))
            {
                lowJump = false;
                rb.gravityScale = lowJumpGravityScale;
            }
                
        }
    }

    private void OnEnable()
    {
        controls.Enable();
    }

    private void OnDisable()
    {
        controls.Disable();
    }
}
