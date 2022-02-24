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
    [SerializeField]
    private GameObject player;
    private PlayerShoot playershoot;

    private void Start() {
        playershoot = player.GetComponent<PlayerShoot>();
    }


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
            playershoot.currentAbility = CurrentEnemy();
        }
       
    }

    private Ability CurrentEnemy()
    {
        Debug.Log(this.gameObject.name);
        if(this.gameObject.name == "FireMage")
        {
            Debug.Log("FireBall!!!");
            return Ability.Fireball;
        } 
        else if(transform.parent.name == "Archer") 
        {
            return Ability.Bow;
        } else {
            return Ability.None;
        }
    }

}
