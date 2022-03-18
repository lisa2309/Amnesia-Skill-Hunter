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
                StateController.defeatedMiniBoss = true;
                SceneManager.LoadScene("Graveyard_Middle_MiniBoss");
            }
        }
        else if (StateController.defeatedMiniBoss)
        {
            StateController.defeatedMiniBoss = false;
            SceneManager.LoadScene("Graveyard_middle_Dorfbewohner");
            StateController.lastLevel = true;
        }
        else if(StateController.lastLevel)
        {
            StateController.lastLevel = false;
            SceneManager.LoadScene("DorfMitGerettetemDorfbewohner");
        }
        else if(SceneManager.GetActiveScene().name == "DorfMitGerettetemDorfbewohner")
        {
            SceneManager.LoadScene("Credits");
        }
        else
        {
            SceneManager.LoadScene("DorfTest");
        }
    }

    public void OnPlayerDeath()
    {
        StateController.resetLevelState();
        StateController.resetPlayerStats();
        SceneManager.LoadScene("DorfTest");
    }

    public void LoadRandomMiddleGraveyardSceneNoBoss()
    {
        SceneManager.LoadScene(GraveyardMiddleVariationsNoBoss[Random.Range(0, GraveyardMiddleVariationsNoBoss.Count - 1)]);
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}