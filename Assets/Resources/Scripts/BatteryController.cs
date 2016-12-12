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
	private GameObject moneyLossDescFab;
    private float moneyStart;
    private bool batteryDead;
    GameObject batteryUI;
    private Text warning;

    float damageTimer;
    float damageTimeout = .15f;

    // Use this for initialization
    private void Start()
    {
        damageTimer = Time.time;
        batteryDead = false;
        moneyStart = UpgradeManager.money;
        dirtCounter = GameObject.Find("DirtCounter");
        batteryUI = GameObject.Find("BatteryUI");
        roombaData = GetComponent<RoombaData>();
        moneyLossFab = Resources.Load("Prefabs/MoneyLoss") as GameObject;
		moneyLossDescFab = Resources.Load("Prefabs/MoneyLossDesc") as GameObject;
        warning = GameObject.Find("RechargeWarning").GetComponent<Text>();

        roombaData.curBatteryPerc = (roombaData.baseBatteryLife + UpgradeManager.Instance.GetUpgradeValue(UpgradeManager.UpgradeEnum.ENERGY) * 10f) / 100f;
        float f = (roombaData.baseBatteryLife + UpgradeManager.Instance.GetUpgradeValue(UpgradeManager.UpgradeEnum.ENERGY) * 10) / roombaData.baseBatteryLife;
        roombaData.curBatteryTime = roombaData.batteryDuration * f;
        startTime = MyTime.Instance.time;
    }

    // Update is called once per frame
    private void Update()
    {
        //roombaData.curBatteryPerc = (1 - (MyTime.Instance.time - startTime) / roombaData.curBatteryTime) * ((roombaData.baseBatteryLife + UpgradeManager.Instance.GetUpgradeValue(UpgradeManager.UpgradeEnum.ENERGY) * 10f) / 100f);
        roombaData.curBatteryPerc = (1 - (MyTime.Instance.time - startTime) / roombaData.curBatteryTime) * ((roombaData.baseBatteryLife + UpgradeManager.Instance.GetUpgradeValue(UpgradeManager.UpgradeEnum.ENERGY) * UpgradeManager.Instance.GetUpgradeValue(UpgradeManager.UpgradeEnum.ENERGY) * 10f) / 100f);

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

			GameObject descTxt = Instantiate(moneyLossDescFab) as GameObject;
			descTxt.transform.SetParent(dirtCounter.transform.parent);
			descTxt.transform.position = dirtCounter.transform.position + Vector3.right * 139 + Vector3.up * 70;

            warning.enabled = false;
            UpgradePanelShowHide.instance.ShowHide(true);
        }
    }

    public void Damage()
    {
        if (Time.time - damageTimer > damageTimeout)
        {
            damageTimer = Time.time;
            int dmgAmt = (6 - UpgradeManager.Instance.GetUpgradeValue(UpgradeManager.UpgradeEnum.DURABILITY));

            GameObject dmgTxt = Instantiate(moneyLossFab) as GameObject;
            dmgTxt.GetComponent<Text>().text = "-" + dmgAmt + "%";
            dmgTxt.transform.SetParent(batteryUI.transform.parent);
            dmgTxt.transform.position = batteryUI.transform.position + Vector3.right * 50;

            startTime -= dmgAmt;
        }
    }
}