using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    //state
    private int currentHealth;
    public ProgressBar pb;

    //config
    [SerializeField]
    public int maxHealth = 10;

    private void Start()
    {
        currentHealth = maxHealth;
    }

    private void Update(){
        pb.BarValue = currentHealth;
    }

    public bool LooseHealth(int damage)
    {
        currentHealth -= damage;
        return currentHealth <= 0;
    }
}