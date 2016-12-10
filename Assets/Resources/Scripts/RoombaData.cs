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
	public float maxBatteryLife;
	[HideInInspector]
	public float batteryTimer;
	public float batteryDuration;
	public int suctionPower;
    
    // Use this for initialization
    void Start () {
		batteryLife = maxBatteryLife + UpgradeManager.Instance.GetUpgradeValue(UpgradeManager.UpgradeEnum.ENERGY);
		batteryTimer = Time.time;
	}
}
