using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    //state
    private int currentHealth;
    

    //config
    [SerializeField]
    private int maxHealth = 3;

    private Animator animator;

    public void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        currentHealth = maxHealth;
    }

    public bool LooseHealth(int damage)
    {
        currentHealth -= damage;
        Debug.Log("Got Damaged: " + damage);
        animator.SetTrigger("Hit");
        return currentHealth <= 0;
    }
}