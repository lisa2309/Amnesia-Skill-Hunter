using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    //cached references
    //private PlayerControls controls;
    private Animator animator;
    //private PlayerMovement playerMovement;

    //state
    private bool shooting;
    private Coroutine currentSpawnBulletInstance;

    //config
    [Header("Shooting")]
    [SerializeField]
    private float bulletSpawnInterval = 0.5f;
    [SerializeField]
    private float shootingRunModiefier = 0.66f;


    [Header("Manual References")]
    [SerializeField]
    private GameObject bulletPrefab;
    [SerializeField]
    private Transform shootPoint;

   

    private void Awake()
    {/*
        controls = new PlayerControls();

        controls.Gameplay.Shoot.performed += context => StartShooting();
        controls.Gameplay.Shoot.canceled += context => StopShooting();*/
    }

    private void Start()
    {
        animator = GetComponent<Animator>();
        //playerMovement = GetComponent<PlayerMovement>();
    }

    private void StartShooting()
    {
        shooting = true;
        currentSpawnBulletInstance = StartCoroutine(SpawnBullet());
        Debug.Log("HALLOOO");
        //playerMovement.SetRunSpeedModifier(shootingRunModiefier);

        //animation
        animator.SetBool("Croushing", true);

        
    }

    private void StopShooting()
    {
        shooting = false;
        StopCoroutine(currentSpawnBulletInstance);
        //playerMovement.ResetRunspeedModifier();
        //animation
        animator.SetBool("Croushing", false);
    }

    private IEnumerator SpawnBullet()
    {
        Instantiate(bulletPrefab, shootPoint.position, shootPoint.rotation);
        yield return new WaitForSeconds(bulletSpawnInterval);
        if (shooting) StartCoroutine(SpawnBullet());
    }
    /*
    private void OnEnable()
    {
        controls.Enable();
    }

    private void OnDisable()
    {
        controls.Disable();
    }
    */

}
