using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleBehavior : StateMachineBehaviour
{
    [SerializeField]
    private float detectionRange;

    private float idleTimer;
    private Transform playerPosition;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        idleTimer = Random.Range(5f, 10f);
        playerPosition = GameObject.FindGameObjectWithTag("Player").transform;
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (Vector2.Distance(animator.transform.position, playerPosition.position) <= detectionRange)
            animator.SetBool("IsNearPlayer", true);

        if (idleTimer <= 0)
            OnStateExit(animator, stateInfo, layerIndex);
        else
            idleTimer -= Time.deltaTime;
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (idleTimer <= 0)
            animator.SetBool("Patrolling", true);
    }
}
