using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Arrow : MonoBehaviour
{
    //cached references
    private Rigidbody2D rb;

    //config
    [SerializeField]
    private float velocity = 10.0f;
    [SerializeField]
    private int damage = 1;
    [SerializeField]
    private LayerMask stoppingLayers;
    [SerializeField]
    private LayerMask targetLayers;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + new Vector2(transform.right.x, 0.0f) * velocity * Time.fixedDeltaTime);
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ((stoppingLayers.value & (1 << collision.gameObject.layer)) > 0)
        {
            Destroy(gameObject);
        }
        else if ((targetLayers.value & (1 << collision.gameObject.layer)) > 0)
        {
            //damage target
            //EnemyController hitEnemy = collision.gameObject.GetComponent<EnemyController>();
            EnemyHealth enemyHealth = collision.gameObject.GetComponent<EnemyHealth>();
            if (enemyHealth != null)
            {
                enemyHealth.LooseHealth(damage);
                //Destroy(enemyHealth.gameObject);
                //FindObjectOfType<LevelLoader>().DecrementEnemyCount();
            }
            else
            {
                if (collision.gameObject.GetComponent<PlayerHealth>().LooseHealth(damage))
                {
                    FindObjectOfType<LevelLoader>().OnPlayerDeath();
                }

            }
            Destroy(gameObject);
            Debug.Log("target hit");
        }
    }
}
