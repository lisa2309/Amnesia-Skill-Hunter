using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Archer : MonoBehaviour
{
    //cached references
    private Rigidbody2D rb;
    private Animator animator;

    //state
    private bool shooting = false;
    private Coroutine currentSpawnBulletInstance;
    private float guiMoveSpeed = 0.0f;
    //private bool isAlive;
    private int shootCounter = 0;
    private bool runningAway = false;
    

    //config
    [Header("Movement Parameters")]
    [SerializeField]
    private float moveSpeed = 100.0f;
    [SerializeField]
    private float turnDistance = 1.0f;
    [SerializeField]
    private LayerMask obstacles;
    [SerializeField]
    private float minDistanceToPlayer = 10.0f;
    [SerializeField]
    private float runDistance = 5.0f;


    [Header("Shooting Parameters")]
    [SerializeField]
    private float bulletSpawnInterval = 0.5f;

    [SerializeField]
    private float cooldown = 2.0f;
    [SerializeField]
    private float cooldownFac = 1.5f;
    private float lastAttacked = -9999.0f;

    [SerializeField]
    private LayerMask targetLayers;
    [SerializeField]
    private LayerMask visibleLayers;
    [SerializeField]
    private GameObject bulletPrefab;
    [SerializeField]
    private float vision;

    [Header("Manual References")]
    [SerializeField]
    private Transform scanPoint;
    [SerializeField]
    private Transform shootPoint;
    [SerializeField]
    private Transform player;




    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        guiMoveSpeed = moveSpeed;
    }

    private void FixedUpdate()
    {
        RunAway();
        //TurnBack();
        if (WallOrGapAhead()) ChangeDirection();
        if(runningAway)
        {
            if (Vector2.Distance(transform.position, player.position) >= runDistance)
            {
                Debug.Log("TurnBack");
                ChangeDirection();
                runningAway = false;
            }
       
        }
        if (PlayerVisible() && !shooting)
        {
            if (Time.time > lastAttacked + cooldown)
            {
                moveSpeed = 0.0f;
                animator.SetTrigger("ShootTrig");
                Debug.Log("ShootTrig");

                Instantiate(bulletPrefab, shootPoint.position, shootPoint.rotation);
                lastAttacked = Time.time;
            }
        }
        else if(!PlayerVisible() && Time.time > lastAttacked + (cooldown / cooldownFac)) // Falls es stoert das der Archer beim Raumgewinn wartet nur cooldown!!!
        {
            moveSpeed = guiMoveSpeed;
        }
        //else if (!PlayerVisible() && shooting) StopShooting();
        Move();
    }

    private void Move()
    {
            float horizontalVelocity = transform.right.x * moveSpeed * Time.fixedDeltaTime;
            rb.velocity = new Vector2(horizontalVelocity, rb.velocity.y);

            //animation
            animator.SetFloat("HorizontalSpeed", Mathf.Abs(horizontalVelocity));
    }

    private void ChangeDirection()
    {
        if (transform.eulerAngles == Vector3.zero) transform.eulerAngles = new Vector3(0.0f, 180.0f, 0.0f);
        else transform.eulerAngles = Vector3.zero;
    }
    private bool WallOrGapAhead()
    {
        RaycastHit2D wallHit = Physics2D.Raycast(scanPoint.position, transform.right, turnDistance, obstacles);
        RaycastHit2D floorHit = Physics2D.Raycast(scanPoint.position, -transform.up, scanPoint.localPosition.y + 5.0f, obstacles);
        return wallHit.collider != null || floorHit.collider == null;
    }
    private bool PlayerVisible()
    {
        bool playerHit = false;
        RaycastHit2D hit = Physics2D.Raycast(scanPoint.position, transform.right, vision, visibleLayers);
        if (hit.collider != null)
        {
            if ((targetLayers.value & (1 << hit.collider.gameObject.layer)) > 0)
            {
                playerHit = true;
            }
        }
        return playerHit;
    }
    private void StartShooting()
    {
        if (Time.time > lastAttacked + cooldown)
        {
            shooting = true;
            animator.SetBool("Shooting", true);
            moveSpeed = 0.0f; // Ueberschreibt die Moeglichkeit die Geschwindigkeit in Unity ein zu stellen
            currentSpawnBulletInstance = StartCoroutine(SpawnBullet());
            lastAttacked = Time.time;
        }

    }
    private void StopShooting()
    {
        shooting = false;
        animator.SetBool("Shooting", false);

        moveSpeed = guiMoveSpeed; // Ueberschreibt die Moeglichkeit die Geschwindigkeit in Unity ein zu stellen


        StopCoroutine(currentSpawnBulletInstance);
    }
    private IEnumerator SpawnBullet()
    {
        Instantiate(bulletPrefab, shootPoint.position, shootPoint.rotation);
        yield return new WaitForSeconds(bulletSpawnInterval);
        if (shooting)
        {
            StartCoroutine(SpawnBullet());
            Debug.Log("shootCounter = " + shootCounter);
        }
    }


    private void RunAway()
    {
        if( Vector2.Distance(transform.position, player.position) < minDistanceToPlayer && PlayerVisible())
        {
            runningAway = true;
            Debug.Log("RunAway");
            ChangeDirection();
        }
        

    }

    

  


   
}
