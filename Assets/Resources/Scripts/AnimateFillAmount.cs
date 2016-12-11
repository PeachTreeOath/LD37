using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnimateFillAmount : MonoBehaviour
{
    public Image image;
    private Text text;
    private RoombaData rd;

	float startBatteryPerc;

    // Use this for initialization
    private void Start()
    {
        rd = GameObject.Find("RoombaUnit").GetComponent<RoombaData>();

        image = GetComponent<Image>();
        text = GetComponentInChildren<Text>();

        image.fillAmount = (rd.baseBatteryLife + UpgradeManager.instance.GetUpgradeValue(UpgradeManager.UpgradeEnum.ENERGY) * 10f) / 100f;
		startBatteryPerc = image.fillAmount;
    }

    // Update is called once per frame
    private void Update()
    {
		image.fillAmount = Mathf.Min(rd.curBatteryPerc + Mathf.Lerp(0, .05f, 1-Mathf.InverseLerp(0, startBatteryPerc, rd.curBatteryPerc)), startBatteryPerc); //load bar is off at under 5%
		text.text = (int)(rd.curBatteryPerc * 100) + "%";

		if (rd.curBatteryPerc < .13f &&
            transform.parent.gameObject.GetComponent<BounceScaler>() == null)
        {
            BounceScaler bs = transform.parent.gameObject.AddComponent<BounceScaler>();
            bs.mult = 1.5f;
            bs.speed = 1.3f;
            image.color = Color.yellow;
        }
    }
}