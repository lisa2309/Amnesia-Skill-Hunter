
using UnityEngine;

public class EndTrigger : MonoBehaviour
{

    public LevelLoader levelLoader;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision.gameObject.tag);
        if (collision.gameObject.tag == "Player") levelLoader.LoadNextLevel();
        else if (collision.gameObject.tag == "zuRettenderVillager") Destroy(collision.gameObject);
    }
}

