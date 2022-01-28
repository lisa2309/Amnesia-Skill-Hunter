using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
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
            Enemy hitEnemy = collision.gameObject.GetComponent<Enemy>();
            if (hitEnemy != null)
            {
                Destroy(hitEnemy.gameObject);
                FindObjectOfType<LevelLoader>().DecrementEnemyCount();
            }
            else
            {
                if (collision.gameObject.GetComponent<PlayerHealth>().LooseHealth(damage))
                {
                    FindObjectOfType<LevelLoader>().RestartLevel();
                }
            }
            Destroy(gameObject);
        }
    }
}