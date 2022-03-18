using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    [SerializeField]
    LevelLoader levelloader;
    [SerializeField]
    private AudioSource startMusic;

    public void PlayGame()
    {
        StartCoroutine(music());
        
    }

    public void QuitGame()
    {
        Debug.Log("Quit");
        Application.Quit();
    }

     IEnumerator music(){
         startMusic.Play ();
         yield return new WaitForSeconds(2);
         levelloader.StartGame();
     }
}
