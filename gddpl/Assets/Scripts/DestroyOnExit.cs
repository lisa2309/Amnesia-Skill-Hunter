using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnExit : StateMachineBehaviour
{
        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        Vector3 vec = new Vector3(0.0f, 0.0f, 0.0f) - animator.gameObject.transform.localEulerAngles;
        animator.gameObject.transform.Rotate(vec);
        Destroy(animator.gameObject, stateInfo.length);
    }
}
