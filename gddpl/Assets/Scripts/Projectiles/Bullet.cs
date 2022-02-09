using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
    private Animator animator;
    bool isTriggered = false;
    [SerializeField]
    private int damageRange = 2;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }
    private void FixedUpdate()
    {
        if(!isTriggered){
            rb.MovePosition(rb.position + new Vector2(transform.right.x, 0.0f) * velocity * Time.fixedDeltaTime);
        }
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        CircleCollider2D coll = gameObject.GetComponent<CircleCollider2D>();
        coll.radius = damageRange;
        isTriggered = true;

        if ((stoppingLayers.value & (1 << collision.gameObject.layer)) > 0)
        {
            animator.SetTrigger("Expliosen");
        }
        else if ((targetLayers.value & (1 << collision.gameObject.layer)) > 0)
        {
            Enemy hitEnemy = collision.gameObject.GetComponent<Enemy>();
            if (hitEnemy != null)
            {
                Destroy(hitEnemy.gameObject);
                //FindObjectOfType<LevelLoader>().DecrementEnemyCount();
            }
            else
            {
                if (collision.gameObject.GetComponent<PlayerHealth>().LooseHealth(damage))
                {
                    SceneManager.LoadScene("Lisa's Scene");
                    //FindObjectOfType<LevelLoader>().RestartLevel();
                }
            }
            animator.SetTrigger("Expliosen");
        }
    }
}