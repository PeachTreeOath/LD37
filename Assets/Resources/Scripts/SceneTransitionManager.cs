using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class SceneTransitionManager : Singleton<SceneTransitionManager>
{

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
}
