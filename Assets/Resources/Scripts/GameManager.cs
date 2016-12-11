using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{

    public string persistSceneName = "PersistentUpgrades";

    protected override void Awake()
    {
        base.Awake();

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

    // Use this for initialization
    void Start()
    {
        AudioManager.instance.PlayRoomMusic(.25f);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
