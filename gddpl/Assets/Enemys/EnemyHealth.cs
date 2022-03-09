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
    private GameObject player;
    private PlayerShoot playershoot;

    private void Start() {
        player = GameObject.FindWithTag("Player");
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
            StateController.currentAbility = CurrentEnemy();
        }
       
    }

    private StateController.Ability CurrentEnemy()
    {
        Debug.Log(this.gameObject.name);
        if(this.gameObject.tag == "FireMage")
        {
            Debug.Log("FireBall!!!");
            return StateController.Ability.Fireball;
        } 
        else if(this.gameObject.tag == "Archer") 
        {
            return StateController.Ability.Bow;
        } else {
            return StateController.Ability.None;
        }
    }

}
