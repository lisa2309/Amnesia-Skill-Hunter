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
            yield return new WaitForSeconds(3);
            int rdm = Random.Range(1, 11);
            Debug.Log(rdm);
            if (rdm < 4) Jump();
            if (rdm < 7 && rdm > 3) AttackA();
            if (rdm < 10 && rdm > 6) AttackB();
            if (rdm == 10) Die();
        }
    }
    private void Jump()
    {
        Debug.Log("Jumping");
        animator.SetTrigger("jump");
    }

    private void AttackA()
    {
        Debug.Log("AttackA");
        animator.SetTrigger("attackA");
        rb.velocity = new Vector2(0, rb.velocity.y);
    }

    private void AttackB()
    {
        Debug.Log("AttackB");
        animator.SetTrigger("attackB");
        rb.velocity = new Vector2(0, rb.velocity.y);
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
