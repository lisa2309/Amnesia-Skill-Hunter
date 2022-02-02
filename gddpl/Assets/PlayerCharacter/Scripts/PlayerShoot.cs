using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    //cached references
    //private PlayerControls controls;
    private Animator animator;
    private PlayerMovement movement;
    private Controls controls;

    //state
    private bool shooting;
    private Coroutine currentSpawnBulletInstance;
    private Ability currentAbility = Ability.None;

    //config
    [Header("Shooting")]
    [SerializeField]
    private float bulletSpawnInterval = 0.5f;
    [SerializeField]
    private float shootingRunModifier = 0.66f;

    [Header("Manual References")]
    [SerializeField]
    private GameObject bulletPrefab;
    [SerializeField]
    private Transform shootPoint;

    private void Awake()
    {
        controls = new Controls();

        controls.Gameplay.Power.performed += context => StartShooting();
        controls.Gameplay.Power.canceled += context => StopShooting();
    }

    private void Start()
    {
        animator = GetComponent<Animator>();

        movement = GetComponent<PlayerMovement>();
    }

    private void StartShooting()
    {
        switch (currentAbility)
        {
            case Ability.Fireball:
                ShootFireball();
                break;
            case Ability.Bow:
                ShootBow();
                break;
            case Ability.Dash:
                Dash();
                break;
            case Ability.Stamp:
                Stamp();
                break;
            default:
                break;
        }
    }

    private void ShootFireball()
    {
        shooting = true;
        currentSpawnBulletInstance = StartCoroutine(SpawnBullet());
        Debug.Log("HALLOOO");
        //playerMovement.SetRunSpeedModifier(shootingRunModiefier);

        //animation
        animator.SetBool("Croushing", true);

        movement.SetRunSpeedModifier(shootingRunModifier);

        //animation
        animator.SetBool("Shooting", true);
    }

    private void ShootBow()
    {

    }

    private void Dash()
    {

    }

    private void Stamp()
    {

    }

    private void StopShooting()
    {
        shooting = false;
        StopCoroutine(currentSpawnBulletInstance);

        movement.ResetRunSpeedModifier();

        switch (currentAbility)
        {
            case Ability.Fireball:
                StopShootFireball();
                break;
            case Ability.Bow:
                StopShootBow();
                break;
            case Ability.Dash:
                StopDash();
                break;
            case Ability.Stamp:
                StopStamp();
                break;
            default:
                break;
        }
    }

    private void StopShootFireball()
    {
        animator.SetBool("Croushing", false);
    }

    private void StopShootBow()
    {

    }

    private void StopDash()
    {

    }

    private void StopStamp()
    {

    }

    private IEnumerator SpawnBullet()
    {
        Instantiate(bulletPrefab, shootPoint.position, shootPoint.rotation);
        yield return new WaitForSeconds(bulletSpawnInterval);
        if (shooting) StartCoroutine(SpawnBullet());
    }

    private void GetAbilty(Ability ability)
    {
        this.currentAbility = ability;
    }

    private void OnEnable()
    {
        controls.Enable();
    }
    private void OnDisable()
    {
        controls.Disable();
    }
}

public enum Ability
{
    Fireball,
    Bow,
    Dash,
    Stamp,
    None
}