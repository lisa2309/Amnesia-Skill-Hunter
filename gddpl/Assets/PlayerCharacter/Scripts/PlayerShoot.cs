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
    public Ability currentAbility = Ability.Dash;

    private float cooldown = 2.0f;
    private float lastAttacked = -9999.0f;

    //config
    [Header("Shooting")]
    [SerializeField]
    private float bulletSpawnInterval = 0.5f;
    [SerializeField]
    private float shootingRunModifier = 0.66f;
    //[SerializeField]
    //private Camera camera;




    [Header("Manual References")]
    [SerializeField]
    private GameObject bulletPrefab;
    [SerializeField]
    private GameObject arrowPrefab;
    [SerializeField]
    private Transform shootPoint;
    private Vector2 inputMouse;
    Vector2 direction;

    private void Awake()
    {
        controls = new Controls();

        controls.Gameplay.Power.performed += context => StartShooting();
        controls.Gameplay.Power.canceled += context => StopShooting();
        controls.Gameplay.MousePosition.performed += x => inputMouse = x.ReadValue<Vector2>();
    }

    private void Start()
    {
        animator = GetComponent<Animator>();
        movement = GetComponent<PlayerMovement>();
    }

    private void Update()
    {
        Vector3 mousePos = inputMouse;
        Vector3 shootPos = Camera.main.WorldToScreenPoint(shootPoint.position);
       // Vector3 shootPos = GetComponent<Camera>().ScreenToWorldPoint(shootPoint.position);
        mousePos.x = mousePos.x - shootPos.x;
        mousePos.y = mousePos.y - shootPos.y;
        direction = new Vector2(mousePos.x, mousePos.y);
        float shootAngle = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg;
        if(inputMouse.x < shootPoint.position.x)
        {
            shootPoint.rotation = Quaternion.Euler(new Vector3(180f, 0f, -shootAngle));
        } else {
            shootPoint.rotation = Quaternion.Euler(new Vector3(0f, 0f, shootAngle));
        }
    }

    private void StartShooting()
    {
        switch (currentAbility)
        {
            case Ability.Fireball:
                ShootFireball();
                Debug.Log("FireBall");
                break;
            case Ability.Bow:
                ShootBow();
                Debug.Log("BOOOOOOOOOOOW");
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
            Debug.Log("Shoot");
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
        if (Time.time > lastAttacked + cooldown)
        {
            Debug.Log("Shoot");
            shooting = true;
            currentSpawnBulletInstance = StartCoroutine(SpawnArrow());
            //playerMovement.SetRunSpeedModifier(shootingRunModiefier);

            //animation
            animator.SetBool("Croushing", true);

            movement.SetRunSpeedModifier(shootingRunModifier);

            lastAttacked = Time.time;
        }
        
    }

    private void Dash()
    {
        var mousepostionRaw = (Vector3)controls.Gameplay.MousePosition.ReadValue<Vector2>();
        mousepostionRaw.z = 1.0f;
        var mousePositionWorld = GetComponent<Camera>().ScreenToWorldPoint(mousepostionRaw);
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
        animator.SetBool("Croushing", false);
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

    private IEnumerator SpawnArrow()
    {
        Instantiate(arrowPrefab, shootPoint.position, shootPoint.rotation);
        yield return new WaitForSeconds(bulletSpawnInterval);
        if (shooting) StartCoroutine(SpawnArrow());
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