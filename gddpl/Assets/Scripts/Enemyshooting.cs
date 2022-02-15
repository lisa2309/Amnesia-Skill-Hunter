using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemyshooting : MonoBehaviour

{

    public float speed;

    public float stoppingDistance;

    public float retreaDistance;

    private float timeBtwShots;

    public float startTimeBtwShots;

    public Transform shootPoint;

    private bool shooting = false;

    public Transform player;

    public GameObject projectile;

    private Animator animator;

    private Coroutine currentSpawnBulletInstance;



    // Start is called before the first frame update

    void Start()

    {

        // player = GameObject.FindGameObjectWithTag("Player").transform;

         timeBtwShots = startTimeBtwShots;

         animator = GetComponent<Animator>();

    }



    // Update is called once per frame

    void Update()
    {
        if(isAllowed())
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
        }
    }

    private bool isAllowed(){
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("ShootFire")) return false;
        return true;
    }

    private void StartShooting()
    {
        Debug.Log("Shoot");
            shooting = true;

            //animation
            animator.SetBool("Shoot", true);

            currentSpawnBulletInstance = StartCoroutine(SpawnBullet());
    }
    private void StopShooting()
    {
            shooting = false;

            animator.SetBool("Shoot", false);

            StopCoroutine(currentSpawnBulletInstance);    
    }

    private IEnumerator SpawnBullet()
    {
        Instantiate(projectile, shootPoint.position, shootPoint.rotation);
        yield return new WaitForSeconds(timeBtwShots);
        if (shooting) StartCoroutine(SpawnBullet());
    } 

    

}
