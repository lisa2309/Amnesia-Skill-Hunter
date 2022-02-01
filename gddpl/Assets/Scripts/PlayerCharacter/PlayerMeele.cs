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
        if (Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            Attack();
        }
       
        Debug.Log(attacking);
    }

    private void Attack()
    {
        Debug.Log("ATTACK!");
        animator.SetTrigger("Attack");
        var hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

        foreach(var enemy in hitEnemies)
        {
            //Damage enemies
        }
    }
}