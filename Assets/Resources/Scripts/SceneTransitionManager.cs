using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransitionManager : Singleton<SceneTransitionManager>
{
    public void StartGame()
    {
        SceneManager.UnloadSceneAsync("MainMenu");
        SceneManager.LoadScene("Game", LoadSceneMode.Additive);
    }

    public void GoToShop()
    {
        SceneManager.UnloadSceneAsync("Game");
        SceneManager.LoadScene("UpgradeScreen", LoadSceneMode.Additive);
    }

    public void GoToRoom()
    {
        SceneManager.UnloadSceneAsync("UpgradeScreen");
        SceneManager.LoadScene("Game", LoadSceneMode.Additive);
    }

    public void ReloadRoom()
    {
        SceneManager.UnloadSceneAsync("Game");
        SceneManager.LoadScene("Game", LoadSceneMode.Additive);
    }
}