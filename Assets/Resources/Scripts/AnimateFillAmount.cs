using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnimateFillAmount : MonoBehaviour
{
    public Image image;
    private Text text;
    private RoombaData rd;
    // If you change this value, change it in ArrowPointer too
    private float batteryLimit = .15f;
    private Text warning;
    private bool isLowBatt;

    float startBatteryPerc;

    // Use this for initialization
    private void Start()
    {
        rd = GameObject.Find("RoombaUnit").GetComponent<RoombaData>();
        warning = GameObject.Find("RechargeWarning").GetComponent<Text>();
        image = GetComponent<Image>();
        text = GetComponentInChildren<Text>();

        image.fillAmount = (rd.baseBatteryLife + UpgradeManager.Instance.GetUpgradeValue(UpgradeManager.UpgradeEnum.ENERGY) * 10f) / 100f;
		startBatteryPerc = image.fillAmount;
    }

    // Update is called once per frame
    private void Update()
    {
		image.fillAmount = Mathf.Min(rd.curBatteryPerc + Mathf.Lerp(0, .05f, 1-Mathf.InverseLerp(0, startBatteryPerc, rd.curBatteryPerc)), startBatteryPerc); //load bar is off at under 5%
		text.text = (int)(rd.curBatteryPerc * 100) + "%";

        if(!isLowBatt && rd.curBatteryPerc < batteryLimit)
        {
            isLowBatt = true;
            warning.enabled = true;
        }

        if (isLowBatt &&
            transform.parent.gameObject.GetComponent<BounceScaler>() == null)
        {
            BounceScaler bs = transform.parent.gameObject.AddComponent<BounceScaler>();
            bs.mult = 1.5f;
            bs.speed = 1.3f;
            image.color = Color.yellow;
        }
    }
}