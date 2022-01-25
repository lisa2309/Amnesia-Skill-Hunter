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

    private void Start()
    {
        currentHealth = maxHealth;
    }

    public bool LooseHealth(int damage)
    {
        currentHealth -= damage;
        return currentHealth <= 0;
    }
}
