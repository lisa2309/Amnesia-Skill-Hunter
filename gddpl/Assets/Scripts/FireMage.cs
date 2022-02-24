using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireMage : MonoBehaviour

{
    private Animator animator;

    [Header("Movement Parameters")]
    [SerializeField]
    private float speed;
    [SerializeField]
    private float turnDistance = 1.0f;
    [SerializeField]
    private LayerMask obstacles;
    [SerializeField]
    private float stoppingDistance;
    [SerializeField]
    private float retreaDistance;

    [Header("Shooting Parameters")]
    [SerializeField]
    private float timeBtwShots;
    [SerializeField]
    private float startTimeBtwShots;
    [SerializeField]
    private LayerMask targetLayers;
    [SerializeField]
    private LayerMask visibleLayers;
    [SerializeField]
    private GameObject projectile;

    [Header("Manual References")]
    [SerializeField]
    private Transform scanPointFront;
    [SerializeField]
    private Transform scanPointBack;
    [SerializeField]
    private Transform shootPoint;
    [SerializeField]
    private Transform player;

    // Start is called before the first frame update
    void Start()
    {
         timeBtwShots = startTimeBtwShots;

         animator = GetComponent<Animator>();
    }



    // Update is called once per frame
    void Update()
    {
        if(isAllowed())
        {
            if(PlayerVisible())
            {
                ChangeDirection();
            }
            if(WallOrGapAhead() == false)
            {
                if(Vector2.Distance(transform.position, player.position) > stoppingDistance)
                {
                    Debug.Log("Move");
                    transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
                }
                else if(Vector2.Distance(transform.position, player.position) < stoppingDistance 
                && Vector2.Distance(transform.position, player.position) > retreaDistance)
                {
                    Debug.Log("Test");
                    transform.position = this.transform.position;

                    if(timeBtwShots <= 0){
                        animator.SetTrigger("Shoot");

                        Instantiate(projectile, shootPoint.position, shootPoint.rotation);

                        timeBtwShots = startTimeBtwShots;

                    } else {

                        timeBtwShots -= Time.deltaTime;

                    }
                }
                else if (Vector2.Distance(transform.position, player.position) < retreaDistance)
                {
                    Debug.Log("Retreat");
                    transform.position = Vector2.MoveTowards(transform.position, player.position, -speed * Time.deltaTime);
                }
            } else if (WallOrGapAhead() == true) {
                Debug.Log("Wall or Gap");
                transform.position = this.transform.position;
            }
        }
    }

    private bool isAllowed(){
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("ShootFire")) return false;
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Death")) return false;
        return true;
    }

    private bool WallOrGapAhead()
    {
        RaycastHit2D wallHitFront = Physics2D.Raycast(scanPointFront.position, transform.right, turnDistance, obstacles); 
        RaycastHit2D floorHitFront = Physics2D.Raycast(scanPointFront.position, -transform.up, scanPointFront.localPosition.y + 0.5f, obstacles);
        RaycastHit2D wallHitBack = Physics2D.Raycast(scanPointBack.position, transform.right, turnDistance, obstacles); 
        RaycastHit2D floorHitBack = Physics2D.Raycast(scanPointBack.position, -transform.up, scanPointBack.localPosition.y + 0.5f, obstacles);
        return wallHitFront.collider != null /*|| floorHitFront.collider == null */|| wallHitBack.collider != null /*|| floorHitBack.collider == null*/;
    }

    private bool PlayerVisible()
    {
        bool playerHit = false;
        RaycastHit2D hit = Physics2D.Raycast(scanPointBack.position, -transform.right, 100.0f, visibleLayers);
        if (hit.collider != null)
        {
            playerHit = (targetLayers.value & (1 << hit.collider.gameObject.layer)) > 0;
           
        }
        return playerHit;
    }

    private void ChangeDirection()
    {
        if (transform.eulerAngles == Vector3.zero) transform.eulerAngles = new Vector3(0.0f, 180.0f, 0.0f);
        else transform.eulerAngles = Vector3.zero;
    }

}
