using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatteryController : MonoBehaviour {

	RoombaData rd;
	float startTime;

	// Use this for initialization
	void Start () {
		rd = GetComponent<RoombaData>();

		rd.curBatteryPerc = (rd.baseBatteryLife + UpgradeManager.Instance.GetUpgradeValue(UpgradeManager.UpgradeEnum.ENERGY) * 10f)/100f;
		Debug.Log("rd.curBatteryPerc " + rd.curBatteryPerc);
		float f = (rd.baseBatteryLife + UpgradeManager.Instance.GetUpgradeValue(UpgradeManager.UpgradeEnum.ENERGY) * 10)/rd.baseBatteryLife;
		rd.curBatteryTime = rd.batteryDuration * f;
		startTime = Time.time;
		Debug.Log("rd.curBatteryTime " + rd.curBatteryTime);
	}
	
	// Update is called once per frame
	void Update () {
		rd.curBatteryPerc = (1 - (Time.time - startTime) / rd.curBatteryTime) * ((rd.baseBatteryLife + UpgradeManager.Instance.GetUpgradeValue(UpgradeManager.UpgradeEnum.ENERGY) * 10f)/100f);

		CircleCollider2D boxCollider = gameObject.GetComponent<CircleCollider2D>();
		Collider2D[] overlap = Physics2D.OverlapAreaAll(boxCollider.bounds.min, boxCollider.bounds.max);

		if(rd.curBatteryPerc <= 0)
		{
            SceneTransitionManager.instance.GoToShop();
        }
	}

	public void Damage()
	{
		startTime -= (6 - UpgradeManager.Instance.GetUpgradeValue(UpgradeManager.UpgradeEnum.DURABILITY));
	}
}
