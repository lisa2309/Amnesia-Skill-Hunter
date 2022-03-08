using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    //state
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
        if (StateController.currentPlayerHealth == 0)
            StateController.currentPlayerHealth = maxHealth;
    }

    private void Update(){
        progressBar.BarValue = StateController.currentPlayerHealth;
        //Debug.Log(StateController.currentPlayerHealth);
        checkGodMode();
    }

    public float GetCurrentHealth()
    {
        return StateController.currentPlayerHealth;
    }

    public bool LooseHealth(int damage)
    {
        StateController.currentPlayerHealth -= damage;
        animator.SetTrigger("Hit");
        return StateController.currentPlayerHealth <= 0;
    }

    private void checkGodMode()
    {
        if (StateController.isGodModeEnabled)
            StateController.currentPlayerHealth = maxHealth;
    }
}