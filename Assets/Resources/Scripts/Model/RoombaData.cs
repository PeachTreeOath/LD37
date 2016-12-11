using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoombaData : MonoBehaviour {

	public float rotSpeed;
	public float accelSpeed;
	public float minMoveSpeed;
	public float maxMoveSpeed;
    public int health;
	[HideInInspector]
    public float batteryLife;
	public float baseBatteryLife;
	[HideInInspector]
	public float batteryTimer;
	public float batteryDuration;
	public int suctionPower;
	[HideInInspector]
	public float curBatteryPerc;
	[HideInInspector]
	public float curBatteryTime;
    
    // Use this for initialization
    void Start () {
		batteryLife = baseBatteryLife + UpgradeManager.Instance.GetUpgradeValue(UpgradeManager.UpgradeEnum.ENERGY);
		batteryTimer = Time.time;
		//UpgradeManager.Instance.AddUpgrade(UpgradeManager.UpgradeEnum.THORNS, true);
	}
}
