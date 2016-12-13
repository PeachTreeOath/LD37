using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{

    public string persistSceneName = "PersistentUpgrades";

    private int thornsValue = 0;
    private RoombaController roombaController;

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

        roombaController = GameObject.Find("RoombaUnit").GetComponent<RoombaController>();

		UpgradeManager.Instance.GetUpgradeInfo(UpgradeManager.UpgradeEnum.THORNS).value = UpgradeManager.Instance.GetUpgradeInfo(UpgradeManager.UpgradeEnum.THORNS).baseValue;
    }

    // Use this for initialization
    void Start()
    {
        AudioManager.instance.PlayRoomMusic(.25f);

    }

    public bool RemoveThorns()
    {
        if (thornsValue >= 1)
        {
            thornsValue--;
        }

        return thornsValue <= 0;
    }

}
