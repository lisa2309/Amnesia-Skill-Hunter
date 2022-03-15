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

    [SerializeField]
    private float cooldown = 2.0f;
    private float lastAttacked = -9999.0f;

    //config
    [Header("Shooting")]
    [SerializeField]
    private float bulletSpawnInterval = 0.5f;
    [SerializeField]
    private float shootingRunModifier = 0.66f;

    [SerializeField]
    private GameObject villager;

    [SerializeField]
    private float archerFac = 2.0f;




    [Header("Manual References")]
    [SerializeField]
    private GameObject bulletPrefab;
    [SerializeField]
    private GameObject arrowPrefab;
    [SerializeField]
    private Transform shootPointFireBall;
    [SerializeField]
    private Transform shootPointArrow;
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

        if (this.villager.name == "VillageArcher")
        {
            cooldown = cooldown / archerFac;
        }
    }

    private void Update()
    {
        if(StateController.currentAbility == StateController.Ability.Fireball){
            Vector3 mousePos = inputMouse;
            Vector3 shootPos = Camera.main.WorldToScreenPoint(shootPointFireBall.position);
            mousePos.x = mousePos.x - shootPos.x;
            mousePos.y = mousePos.y - shootPos.y;
            direction = new Vector2(mousePos.x, mousePos.y);
            float shootAngle = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg;
            if(inputMouse.x < shootPointFireBall.position.x)
            {
                shootPointFireBall.rotation = Quaternion.Euler(new Vector3(180f, 0f, -shootAngle));
            } else {
                shootPointFireBall.rotation = Quaternion.Euler(new Vector3(0f, 0f, shootAngle));
            }
        }
    }

    private void StartShooting()
    {
        switch (StateController.currentAbility)
        {
            case StateController.Ability.Fireball:
                ShootFireball();
                Debug.Log("FireBall");
                break;
            case StateController.Ability.Bow:
                ShootBow();
                Debug.Log("BOOOOOOOOOOOW");
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

            //animation
            animator.SetBool("Croushing", true);

            movement.SetRunSpeedModifier(shootingRunModifier);
        }

    }

    private void ShootBow()
    {
        shootPointArrow.rotation = Quaternion.Euler(new Vector3(shootPointArrow.eulerAngles.x , shootPointArrow.eulerAngles.y, 0f));
        if (Time.time > lastAttacked + cooldown)
        {
            Debug.Log("Shoot");
            shooting = true; 
            currentSpawnBulletInstance = StartCoroutine(SpawnArrow());

            //animation
            animator.SetBool("Croushing", true);

            movement.SetRunSpeedModifier(shootingRunModifier);

            lastAttacked = Time.time;
        }
        
    }

    private void StopShooting()
    {
        shooting = false;
        StopCoroutine(currentSpawnBulletInstance);

        movement.ResetRunSpeedModifier();

        switch (StateController.currentAbility)
        {
            case StateController.Ability.Fireball:
                StopShootFireball();
                break;
            case StateController.Ability.Bow:
                StopShootBow();
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

    private IEnumerator SpawnBullet()
    {
        Instantiate(bulletPrefab, shootPointFireBall.position, shootPointFireBall.rotation);
        yield return new WaitForSeconds(bulletSpawnInterval);
        if (shooting) StartCoroutine(SpawnBullet());
    }

    private IEnumerator SpawnArrow()
    {
        Instantiate(arrowPrefab, shootPointArrow.position, shootPointArrow.rotation);
        yield return new WaitForSeconds(bulletSpawnInterval);
        if (shooting) StartCoroutine(SpawnArrow());
    }

    private void SetAbility(StateController.Ability ability)
    {
        StateController.currentAbility = ability;
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
