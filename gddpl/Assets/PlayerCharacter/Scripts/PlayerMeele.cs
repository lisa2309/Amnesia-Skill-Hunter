using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMeele : MonoBehaviour
{
    public Transform attackPoint;
    public float attackRange = 0.5f;
    public LayerMask enemyLayers;

    private Animator animator;
    private Controls controls;
    private PlayerMovement playerMovement;
    private bool attacking;

    [SerializeField]
    private float attackCooldown = 1.0f;

    private float lastAttacked = -9999;

    [SerializeField]
    private AudioSource attackSound;


    private void Awake()
    {
        controls = new Controls();
    }

    private void Start()
    {
        animator = GetComponent<Animator>();
        playerMovement = GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Mouse.current.leftButton.wasPressedThisFrame && Time.time > lastAttacked + attackCooldown)
        {
            //Attack();
            animator.SetTrigger("Attack");
            lastAttacked = Time.time;
            
        }
    }

    private void Attack()
    {

        attackSound.Play();
        
        var hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

        foreach(var enemy in hitEnemies)
        {
            
            enemy.GetComponent<EnemyHealth>().LooseHealth(1);
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null) 
            return;

        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
