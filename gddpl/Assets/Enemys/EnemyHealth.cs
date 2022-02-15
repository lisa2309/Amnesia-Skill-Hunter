using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [Header("Basic Parameters")]
    [SerializeField]
    int hitpoints;

    private Animator animator;

    private bool dead;


    private void FixedUpdate()
    {
        EnemyIsDead(); 


    }

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void LooseHealth(int damage)
    {
        hitpoints -= damage;
        animator.SetTrigger("Hit");
    }


    private void EnemyIsDead()
    {
        if(hitpoints <= 0)
        {
            animator.SetBool("Dead", true);
            Debug.Log("Enemy is dead");
        }
       
    }

}
