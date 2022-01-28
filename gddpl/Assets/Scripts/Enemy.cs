using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    //cached references
    private Rigidbody2D rb;
    private Animator animator;

    //state
    private bool shooting = false;
    private Coroutine currentSpawnBulletInstance;

    //config
    [Header("Movement Parameters")]
    [SerializeField]
    private float moveSpeed = 100.0f;
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
    private GameObject bulletPrefab;

    [Header("Manual References")]
    [SerializeField]
    private Transform scanPoint;
    [SerializeField]
    private Transform shootPoint;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        if (WallOrGapAhead()) ChangeDirection();
        if (PlayerVisible() && !shooting) StartShooting();
        else if (!PlayerVisible() && shooting) StopShooting();
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
        RaycastHit2D floorHit = Physics2D.Raycast(scanPoint.position, -transform.up, scanPoint.localPosition.y + 0.5f, obstacles);
        return wallHit.collider != null || floorHit.collider == null;
    }
    private bool PlayerVisible()
    {
        bool playerHit = false;
        RaycastHit2D hit = Physics2D.Raycast(scanPoint.position, transform.right, 100.0f, visibleLayers);
        if (hit.collider != null)
        {
            playerHit = (targetLayers.value & (1 << hit.collider.gameObject.layer)) > 0;
            //if ((targetLayers.value & (1 << hit.collider.gameObject.layer)) > 0)
            //{
            //    playerHit = true;
            //}
        }
        return playerHit;
    }
    private void StartShooting()
    {
        shooting = true;

        currentSpawnBulletInstance = StartCoroutine(SpawnBullet());
    }
    private void StopShooting()
    {
        shooting = false;

        StopCoroutine(currentSpawnBulletInstance);
    }
    private IEnumerator SpawnBullet()
    {
        Instantiate(bulletPrefab, shootPoint.position, shootPoint.rotation);
        yield return new WaitForSeconds(bulletSpawnInterval);
        if (shooting) StartCoroutine(SpawnBullet());
    }
}
