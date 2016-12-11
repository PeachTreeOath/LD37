using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnimateFillAmount : MonoBehaviour
{
    public Image image;
    private Text text;
	RoombaData rd;

    // Use this for initialization
    void Start()
    {
		rd = GameObject.Find("RoombaUnit").GetComponent<RoombaData>();

        image = GetComponent<Image>();
        text = GetComponentInChildren<Text>();

		image.fillAmount = (rd.baseBatteryLife + UpgradeManager.Instance.GetUpgradeValue(UpgradeManager.UpgradeEnum.ENERGY) * 10f)/100f;
    }

    // Update is called once per frame
    void Update()
    {
		image.fillAmount = rd.curBatteryPerc;
        text.text = (int)(image.fillAmount * 100) + "%";
    }
}