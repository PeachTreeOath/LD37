using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BatteryController : MonoBehaviour
{
    private RoombaData rd;
    private float startTime;
	GameObject dirtCounter;
	GameObject moneyLossFab;
	float moneyStart;
	bool batteryDead;

    // Use this for initialization
    private void Start()
    {
		batteryDead = false;
		moneyStart = UpgradeManager.money;
		dirtCounter = GameObject.Find("DirtCounter");
        rd = GetComponent<RoombaData>();
		moneyLossFab = Resources.Load("Prefabs/MoneyLoss") as GameObject;

        rd.curBatteryPerc = (rd.baseBatteryLife + UpgradeManager.Instance.GetUpgradeValue(UpgradeManager.UpgradeEnum.ENERGY) * 10f) / 100f;
        float f = (rd.baseBatteryLife + UpgradeManager.Instance.GetUpgradeValue(UpgradeManager.UpgradeEnum.ENERGY) * 10) / rd.baseBatteryLife;
        rd.curBatteryTime = rd.batteryDuration * f;
        startTime = Time.time;
    }

    // Update is called once per frame
    private void Update()
    {
        rd.curBatteryPerc = (1 - (Time.time - startTime) / rd.curBatteryTime) * ((rd.baseBatteryLife + UpgradeManager.Instance.GetUpgradeValue(UpgradeManager.UpgradeEnum.ENERGY) * 10f) / 100f);

        CircleCollider2D boxCollider = gameObject.GetComponent<CircleCollider2D>();
        Collider2D[] overlap = Physics2D.OverlapAreaAll(boxCollider.bounds.min, boxCollider.bounds.max);

		if (rd.curBatteryPerc <= 0 && !batteryDead)
        {
			batteryDead = true;
            //SceneTransitionManager.instance.GoToShop();
			int moneyDec = (int)((UpgradeManager.money - moneyStart)/2);
            Time.timeScale = 0;
			UpgradeManager.money -= moneyDec;
			GameObject moneyLossTxt = Instantiate(moneyLossFab) as GameObject;
			moneyLossTxt.GetComponent<Text>().text = "-"+moneyDec;
			moneyLossTxt.transform.SetParent(dirtCounter.transform.parent);
			moneyLossTxt.transform.position = dirtCounter.transform.position;

            UpgradePanelShowHide.instance.ShowHide(true);
        }
    }

    public void Damage()
    {
        startTime -= (6 - UpgradeManager.Instance.GetUpgradeValue(UpgradeManager.UpgradeEnum.DURABILITY));
    }
}