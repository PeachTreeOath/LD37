using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SceneCEO : Singleton<SceneCEO>
{

    // List of inspector-set singleton prefabs to be loaded on startup
    public List<GameObject> singletonList;

    protected override void Awake()
    {
        base.Awake();

        foreach (GameObject controller in singletonList)
        {
            if (controller != null)
            {
                GameObject newController = Instantiate<GameObject>(controller);
                newController.transform.SetParent(transform);
            }
        }
    }
}
