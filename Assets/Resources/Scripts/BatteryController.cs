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

        rd.curBatteryPerc = (rd.baseBatteryLife + UpgradeManager.instance.GetUpgradeLevel(UpgradeManager.UpgradeEnum.ENERGY) * 10f) / 100f;
        float f = (rd.baseBatteryLife + UpgradeManager.instance.GetUpgradeLevel(UpgradeManager.UpgradeEnum.ENERGY) * 10) / rd.baseBatteryLife;
        rd.curBatteryTime = rd.batteryDuration * f;
        startTime = Time.time;
    }

    // Update is called once per frame
    private void Update()
    {
        rd.curBatteryPerc = (1 - (Time.time - startTime) / rd.curBatteryTime) * ((rd.baseBatteryLife + UpgradeManager.instance.GetUpgradeLevel(UpgradeManager.UpgradeEnum.ENERGY) * 10f) / 100f);

        CircleCollider2D boxCollider = gameObject.GetComponent<CircleCollider2D>();
        Collider2D[] overlap = Physics2D.OverlapAreaAll(boxCollider.bounds.min, boxCollider.bounds.max);

        if (rd.curBatteryPerc <= 0)
        {
            Application.LoadLevel(1);
        }
    }

    public void Damage()
    {
        startTime -= (6 - UpgradeManager.instance.GetUpgradeLevel(UpgradeManager.UpgradeEnum.DURABILITY));
    }
}