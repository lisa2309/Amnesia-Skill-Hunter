using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingTrigger : MonoBehaviour
{
    [SerializeField]
    private LayerMask playerLayers;

    private void OnTriggerEnter2D(Collider2D collision) {
        if ((playerLayers.value & (1 << collision.gameObject.layer)) > 0)
        {
            Destroy(this.gameObject, 20);
        }
    }
}
