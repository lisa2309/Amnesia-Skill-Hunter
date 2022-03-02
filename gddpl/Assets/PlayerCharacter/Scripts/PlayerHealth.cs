using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    //state
    private int currentHealth;
    private ProgressBar progressBar;

    //config
    [SerializeField]
    public int maxHealth = 10;

    private Animator animator;

    public void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        progressBar = ProgressBar.FindObjectOfType<ProgressBar>();
        currentHealth = maxHealth;
    }

    private void Update(){
        progressBar.BarValue = currentHealth;
    }

    public float GetCurrentHealth()
    {
        return currentHealth;
    }

    public bool LooseHealth(int damage)
    {
        currentHealth -= damage;
        Debug.Log("Got Damaged: " + damage);
        Debug.Log("current health: " + currentHealth);
        animator.SetTrigger("Hit");
        return currentHealth <= 0;
    }
}