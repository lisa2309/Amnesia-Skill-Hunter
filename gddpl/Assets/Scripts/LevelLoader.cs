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

    public void RestartLevel()
    {
        LoadRandomMiddleGraveyardSceneNoBoss();
    }
    public void DecrementEnemyCount()
    {

    }

    public void LoadRandomMiddleGraveyardSceneNoBoss()
    {
        SceneManager.LoadScene(GraveyardMiddleVariationsNoBoss[Random.Range(0, GraveyardMiddleVariationsNoBoss.Count - 1)]);
    }
}