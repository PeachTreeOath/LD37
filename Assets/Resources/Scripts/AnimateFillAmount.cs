using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnimateFillAmount : MonoBehaviour
{
    public Image image;
    public bool coolingDown;
    private Text text;
	RoombaData rd;
	float waitTime;

    // Use this for initialization
    void Start()
    {
		rd = GameObject.Find("RoombaUnit").GetComponent<RoombaData>();

        image = GetComponent<Image>();
        text = GetComponentInChildren<Text>();

		image.fillAmount = (rd.baseBatteryLife + UpgradeManager.Instance.GetUpgradeValue(UpgradeManager.UpgradeEnum.ENERGY) * 10f)/100f;
		float f = (rd.baseBatteryLife + UpgradeManager.Instance.GetUpgradeValue(UpgradeManager.UpgradeEnum.ENERGY) * 10)/rd.baseBatteryLife;
		waitTime = rd.batteryDuration * f;
    }

    // Update is called once per frame
    void Update()
    {
        image.fillAmount -= 1.0f / waitTime * Time.deltaTime;
        text.text = (int)(image.fillAmount * 100) + "%";
        if (coolingDown == true)
        {
            //Reduce fill amount over 30 seconds
            image.fillAmount -= 1.0f / waitTime * Time.deltaTime;
        }
    }
}