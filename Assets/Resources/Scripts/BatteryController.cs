using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BatteryController : MonoBehaviour
{
    private RoombaData roombaData;
    private float startTime;
    private GameObject dirtCounter;
    private GameObject moneyLossFab;
    private float moneyStart;
    private bool batteryDead;

    // Use this for initialization
    private void Start()
    {
        batteryDead = false;
        moneyStart = UpgradeManager.money;
        dirtCounter = GameObject.Find("DirtCounter");
        roombaData = GetComponent<RoombaData>();
        moneyLossFab = Resources.Load("Prefabs/MoneyLoss") as GameObject;

        roombaData.curBatteryPerc = (roombaData.baseBatteryLife + UpgradeManager.Instance.GetUpgradeValue(UpgradeManager.UpgradeEnum.ENERGY) * 10f) / 100f;
        float f = (roombaData.baseBatteryLife + UpgradeManager.Instance.GetUpgradeValue(UpgradeManager.UpgradeEnum.ENERGY) * 10) / roombaData.baseBatteryLife;
        roombaData.curBatteryTime = roombaData.batteryDuration * f;
		startTime = MyTime.Instance.time;
    }

    // Update is called once per frame
    private void Update()
    {
		roombaData.curBatteryPerc = (1 - (MyTime.Instance.time - startTime) / roombaData.curBatteryTime) * ((roombaData.baseBatteryLife + UpgradeManager.Instance.GetUpgradeValue(UpgradeManager.UpgradeEnum.ENERGY) * 10f) / 100f);

        CircleCollider2D boxCollider = gameObject.GetComponent<CircleCollider2D>();
        Collider2D[] overlap = Physics2D.OverlapAreaAll(boxCollider.bounds.min, boxCollider.bounds.max);

        if (roombaData.curBatteryPerc <= 0 && !batteryDead)
        {
            batteryDead = true;
            //SceneTransitionManager.instance.GoToShop();
            int moneyDec = (int)((UpgradeManager.money - moneyStart) / 2);
			MyTime.Instance.timeScale = 0;
            UpgradeManager.money -= moneyDec;
			dirtCounter.GetComponent<Text>().text = "" + UpgradeManager.money;
            GameObject moneyLossTxt = Instantiate(moneyLossFab) as GameObject;
            moneyLossTxt.GetComponent<Text>().text = "-" + moneyDec;
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