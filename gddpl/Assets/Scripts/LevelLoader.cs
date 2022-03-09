using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    [SerializeField]
    private List<string> GraveyardMiddleVariationsNoBoss;

    int currentStage;
    int currentSection;

    private void Start()
    {

    }

    public void StartGame()
    {
        SceneManager.LoadScene("DorfTest");
    }

    public void LoadNextLevel()
    {
        Debug.Log(currentSection);
        if (StateController.currentStage == 0)
        {
            if(StateController.currentSection <= 1)
            {
                StateController.currentSection++;
                Debug.Log(StateController.currentSection);
                LoadRandomMiddleGraveyardSceneNoBoss();
            }
            else
            {
                StateController.currentStage++;
                SceneManager.LoadScene("Graveyard_Middle_MiniBoss");
            }
        }
        else
        {
            SceneManager.LoadScene("DorfTest");
        }
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