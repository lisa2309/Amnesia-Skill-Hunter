using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private Animator animator;
    private Rigidbody2D rb;


    private bool isAlive;
    public float fadeSpeed;
    [Header("Movement")]
    [SerializeField]
    private float movementSpeed;
    [SerializeField]
    private float turnDistance;
    [SerializeField]
    private LayerMask obstacles;
    [SerializeField]
    private Transform scanPoint;

    [Header("Attacks")]
    [SerializeField]
    private ParticleSystem stompLeft;
    [SerializeField]
    private ParticleSystem stompRight;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        isAlive = animator.GetBool("isAlive");
        StartCoroutine(AnimationShowcase());
    }

    private void FixedUpdate()
    {
        if (WallOrGapAhead()) ChangeDirection();
        Move();        
    }

    IEnumerator AnimationShowcase()
    {
        while (isAlive) {
            yield return new WaitForSeconds(5);
            int rdm = Random.Range(1, 14); 
            if (rdm < 4) Jump();
            if (rdm < 7 && rdm > 3) AttackA();
            if (rdm < 10 && rdm > 6) AttackB();
            if (rdm < 13 && rdm > 9) Stomp();
            if (rdm == 13) Die();
        }
    }

    private void Jump()
    {
        Debug.Log("Jumping");
    }
    private void Stomp()
    {
        Debug.Log("Stomping");
        rb.velocity = new Vector2(0, rb.velocity.y);
        animator.SetBool("isStomping", true);
        animator.SetTrigger("jump");
        StartCoroutine("TriggerStomp");
    }

    IEnumerator TriggerStomp()
    {

        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length-0.2f);
        stompLeft.Play();
        stompRight.Play();
        yield return new WaitForSeconds(1.3f);
        animator.SetBool("isStomping", false);
    }

    private void AttackA()
    {
        Debug.Log("AttackA");
        rb.velocity = new Vector2(0, rb.velocity.y);
        animator.SetTrigger("attackA");
        
    }

    private void AttackB()
    {
        Debug.Log("AttackB");
        rb.velocity = new Vector2(0, rb.velocity.y);
        animator.SetTrigger("attackB");
    }

    private void Die()
    {
        Debug.Log("Death");
        isAlive = false;
        rb.velocity = new Vector2(0, rb.velocity.y);
        animator.SetBool("isAlive", isAlive);
        animator.SetTrigger("die");
        Destroy(gameObject, animator.GetCurrentAnimatorStateInfo(0).length + 0.3f);
    }

    private void Move()
    {
        //check if movement is allowed during animation
        if (isMoveAllowed()) {
        float horizontalVelocity = transform.right.x * movementSpeed * Time.fixedDeltaTime;
        rb.velocity = new Vector2(horizontalVelocity, rb.velocity.y);
        animator.SetFloat("horizontalSpeed", Mathf.Abs(horizontalVelocity)); }
    }

    private bool isMoveAllowed()
    {
        if (!isAlive) return false;
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("skelly_stomp")) return false;
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("skelly_jump")) return false;
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("skelly_AttackA")) return false;
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("skelly_AttackB")) return false;
        return true;
    }
    private void ChangeDirection()
    {
        if (transform.eulerAngles == Vector3.zero) transform.eulerAngles = new Vector3(0.0f, 180.0f, 0.0f);
        else transform.eulerAngles = Vector3.zero;
    }

    private bool WallOrGapAhead()
    {
        RaycastHit2D wallHit = Physics2D.Raycast(scanPoint.position, transform.right, turnDistance, obstacles);
        RaycastHit2D floorHit = Physics2D.Raycast(scanPoint.position, -transform.up, scanPoint.localPosition.y + 1.0f, obstacles);
        return floorHit.collider == null || wallHit.collider != null;
    }

}
