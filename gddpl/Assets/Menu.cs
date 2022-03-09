using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    [SerializeField]
    LevelLoader levelloader;

    public void PlayGame()
    {
        levelloader.StartGame();
    }

    public void QuitGame()
    {
        Debug.Log("Quit");
        Application.Quit();
    }
}
