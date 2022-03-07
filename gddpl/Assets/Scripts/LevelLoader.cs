using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    [SerializeField]
    private List<string> GraveyardMiddleVariationsNoBoss;

    private void Start()
    {

    }

    public void StartGame()
    {
        SceneManager.LoadScene("DorfTest");
    }

    public void LoadNextLevel()
    {
        LoadRandomMiddleGraveyardSceneNoBoss();
    }

    public void OnPlayerDeath()
    {
        SceneManager.LoadScene("DorfTest");
    }

    public void LoadRandomMiddleGraveyardSceneNoBoss()
    {
        SceneManager.LoadScene(GraveyardMiddleVariationsNoBoss[Random.Range(0, GraveyardMiddleVariationsNoBoss.Count - 1)]);
    }
}