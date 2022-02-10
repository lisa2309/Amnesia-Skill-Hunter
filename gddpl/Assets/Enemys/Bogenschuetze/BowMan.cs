using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BowMan : MonoBehaviour
{

    //cached references
    private Rigidbody2D rb;
    private Animator animator;
    private EnemyHealth health;

    //state
    private bool shooting = false;
    private Coroutine currentSpawnBulletInstance;
    private Vector3 shootingDirection;


    //config
    [Header("Movement Parameters")]
    [SerializeField]
    private float movementSpeed = 100.0f;
    [SerializeField]
    private float turnDistance = 1.0f;
    [SerializeField]
    private LayerMask obstacles;
    

    [Header("Shooting Parameters")]
    [SerializeField]
    private float bulletSpawnInterval = 0.5f;
    [SerializeField]
    private LayerMask targetLayers;
    [SerializeField]
    private LayerMask visibleLayers;
    [SerializeField]
    private GameObject ArrowPrefab;
    [SerializeField]
    private float vision = 50.0f;
  

    [Header("Manual References")]
    [SerializeField]
    private Transform scanPoint;
    [SerializeField]
    private Transform shootPoint;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        health = GetComponent<EnemyHealth>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
        if (WallOrGapAhead()) ChangeDirection();
        if (PlayerVisible() && !shooting) StartShooting();
        else if (!PlayerVisible() && shooting) StopShooting();
        Move();
    }

    private void ChangeDirection()
    {
        if (transform.eulerAngles == Vector3.zero) transform.eulerAngles = new Vector3(0.0f, 180.0f, 0.0f);
        else transform.eulerAngles = Vector3.zero;
    }



    private void Move()
    {
        float horizontalVelocity = transform.right.x * movementSpeed * Time.fixedDeltaTime;
        rb.velocity = new Vector2(horizontalVelocity, rb.velocity.y);
        animator.SetFloat("horizontalSpeed", Mathf.Abs(horizontalVelocity));
    }

    private bool WallOrGapAhead()
    {
        RaycastHit2D wallHit = Physics2D.Raycast(scanPoint.position, transform.right, turnDistance, obstacles);
        RaycastHit2D floorHit = Physics2D.Raycast(scanPoint.position, -transform.up, scanPoint.localPosition.y + 1.0f, obstacles);
        return floorHit.collider == null || wallHit.collider != null;
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
        shooting = true;

        currentSpawnBulletInstance = StartCoroutine(SpawnArrow());
    }
    private void StopShooting()
    {
        shooting = false;

        StopCoroutine(currentSpawnBulletInstance);
    }

    private IEnumerator SpawnArrow()
    {
        Instantiate(ArrowPrefab, shootPoint.position, shootPoint.rotation);
        yield return new WaitForSeconds(bulletSpawnInterval);
        if (shooting) StartCoroutine(SpawnArrow());
    }
}
