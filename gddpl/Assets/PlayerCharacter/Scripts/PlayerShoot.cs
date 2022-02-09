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
    private Ability currentAbility = Ability.Dash;

    //config
    [Header("Shooting")]
    [SerializeField]
    private float bulletSpawnInterval = 0.5f;
    [SerializeField]
    private float shootingRunModifier = 0.66f;
    [SerializeField]
    private Camera camera;




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
        if(GameObject.Find("FireBall") == null && GameObject.Find("FireBall(Clone)") == null)
        {
            shooting = true;
            currentSpawnBulletInstance = StartCoroutine(SpawnBullet());
            //playerMovement.SetRunSpeedModifier(shootingRunModiefier);

            //animation
            animator.SetBool("Croushing", true);

            movement.SetRunSpeedModifier(shootingRunModifier);
        }

    }

    private void ShootBow()
    {

    }

    private void Dash()
    {
        var mousepostionRaw = (Vector3)controls.Gameplay.MousePosition.ReadValue<Vector2>();
        mousepostionRaw.z = 1.0f;
        var mousePositionWorld = camera.ScreenToWorldPoint(mousepostionRaw);
        var directionVector = mousePositionWorld - this.transform.position;
        movement.startDash(directionVector.normalized);
        Debug.Log("direction: " + directionVector);
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

    private void SetAbility(Ability ability)
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

public enum DashDirection
{
    Left,
    Right,
    None
}