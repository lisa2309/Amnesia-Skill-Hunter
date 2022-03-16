using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportTrigger : MonoBehaviour
{
    private GameObject teleportBarrier;
    // Start is called before the first frame update
    void Start()
    {
         
    }

    // Update is called once per frame
    void Update()
    {
        teleportBarrier = GameObject.Find("MiniBoss");  
        if(teleportBarrier == null){
            Destroy(this.gameObject);
        }
        
    }
}
