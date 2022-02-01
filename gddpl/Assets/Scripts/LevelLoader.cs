using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    //state
    private int enemyCount;

    private void Start()
    {
        enemyCount = FindObjectsOfType<Enemy>().Length;
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void DecrementEnemyCount()
    {
        enemyCount--;
        if (enemyCount <= 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}