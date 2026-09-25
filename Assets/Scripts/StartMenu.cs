using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour
{
    public string gameSceneName = "_Scene_0";   // change to your game scene's exact name

    public void StartGame()
    {
        SceneManager.LoadScene(gameSceneName);
    }
}