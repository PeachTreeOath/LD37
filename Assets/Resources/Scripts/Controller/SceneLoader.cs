using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    float timeLeft = 3.0f;
    bool loadGame = false;

    public void LoadGame()
    {
        loadGame = true;
    }

    void Update()
    {
        if(loadGame)
        {
            timeLeft -= Time.deltaTime;
            Debug.Log(timeLeft);
            if(timeLeft <= -1)
            {
                SceneManager.LoadScene("Game", LoadSceneMode.Single);
            }
        }
    }
}