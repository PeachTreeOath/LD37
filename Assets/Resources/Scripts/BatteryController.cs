using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatteryController : MonoBehaviour
{
    private RoombaData rd;
    private float startTime;

    // Use this for initialization
    private void Start()
    {
        rd = GetComponent<RoombaData>();

        rd.curBatteryPerc = (rd.baseBatteryLife + UpgradeManager.instance.GetUpgradeValue(UpgradeManager.UpgradeEnum.ENERGY) * 10f) / 100f;
        float f = (rd.baseBatteryLife + UpgradeManager.instance.GetUpgradeValue(UpgradeManager.UpgradeEnum.ENERGY) * 10) / rd.baseBatteryLife;
        rd.curBatteryTime = rd.batteryDuration * f;
        startTime = Time.time;
    }

    // Update is called once per frame
    private void Update()
    {
        rd.curBatteryPerc = (1 - (Time.time - startTime) / rd.curBatteryTime) * ((rd.baseBatteryLife + UpgradeManager.instance.GetUpgradeValue(UpgradeManager.UpgradeEnum.ENERGY) * 10f) / 100f);

        CircleCollider2D boxCollider = gameObject.GetComponent<CircleCollider2D>();
        Collider2D[] overlap = Physics2D.OverlapAreaAll(boxCollider.bounds.min, boxCollider.bounds.max);

        if (rd.curBatteryPerc <= 0)
        {
            //SceneTransitionManager.instance.GoToShop();
            Time.timeScale = 0;
            UpgradePanelShowHide.instance.ShowHide(true);
        }
    }

    public void Damage()
    {
        startTime -= (6 - UpgradeManager.instance.GetUpgradeValue(UpgradeManager.UpgradeEnum.DURABILITY));
    }
}