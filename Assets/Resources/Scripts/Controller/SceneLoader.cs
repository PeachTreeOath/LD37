using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public string persistSceneName = "PersistentUpgrades";

    void Awake()
    {
        bool isPersistenceSceneLoaded = false;
        for (int i = 0; i < SceneManager.sceneCount; i++)
        {
            Scene scene = SceneManager.GetSceneAt(i);
            if (scene.name.Equals(persistSceneName))
            {
                isPersistenceSceneLoaded = true;
                break;
            }
        }

        if (!isPersistenceSceneLoaded)
        {
            SceneManager.LoadScene(persistSceneName, LoadSceneMode.Additive);
        }
    }

    public void GotoGame()
    {
        SceneTransitionManager.instance.StartGame();
    }
}