using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeManager : Singleton<UpgradeManager> {

    [HideInInspector]
    public int thornLevel;
    [HideInInspector]
    public int visionLevel;
    [HideInInspector]
    public int energyLevel;
    [HideInInspector]
    public int deepCleanLevel;
    [HideInInspector]
    public int cleanRadiusLevel;
    [HideInInspector]
    public int speedLevel;
    [HideInInspector]
    public int launchSpeedLevel;
    [HideInInspector]
    public int turningLevel;
    public int money = 200;

	
	// Update is called once per frame
	void Update () {
		
	}
}
