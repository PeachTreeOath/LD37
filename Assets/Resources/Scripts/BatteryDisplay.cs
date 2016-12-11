using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BatteryDisplay : MonoBehaviour
{
    private List<GameObject> batteryIcons;
    private RoombaData rd;
    private Color[] cols = { Color.green, Color.yellow, Color.red, Color.black };
    private float maxBatteryTime;
    private float startTime;

    // Use this for initialization
    private void Start()
    {
        rd = GameObject.Find("RoombaUnit").GetComponent<RoombaData>();
        batteryIcons = new List<GameObject>();
        GameObject canvObj = Instantiate(Resources.Load("Prefabs/Canvas")) as GameObject;
        Canvas canv = canvObj.GetComponent<Canvas>();
        float x = canv.GetComponent<RectTransform>().sizeDelta.x;
        Vector2 basePos = Vector2.zero;
        bool setBasePos = false;
        for (int i = 0; i < rd.baseBatteryLife + UpgradeManager.instance.GetUpgradeValue(UpgradeManager.UpgradeEnum.ENERGY); i++)
        {
            GameObject icn = Instantiate(Resources.Load("Prefabs/BatteryIcon")) as GameObject;
            batteryIcons.Add(icn);
            icn.transform.SetParent(canvObj.transform);

            if (!setBasePos)
            {
                setBasePos = true;
                basePos = batteryIcons[0].GetComponent<RectTransform>().anchoredPosition;
            }

            icn.GetComponent<RectTransform>().anchoredPosition = basePos + Vector2.right * (i * icn.GetComponent<RectTransform>().sizeDelta.x / 2f + 2);
            icn.GetComponent<RectTransform>().anchoredPosition = new Vector2(x + icn.GetComponent<RectTransform>().anchoredPosition.x, icn.GetComponent<RectTransform>().anchoredPosition.y);
        }

        float minX = float.MaxValue;
        float maxX = float.MinValue;
        for (int i = 0; i < batteryIcons.Count; i++)
        {
            if (batteryIcons[i].GetComponent<RectTransform>().anchoredPosition.x < minX)
            {
                minX = batteryIcons[i].GetComponent<RectTransform>().anchoredPosition.x;
            }

            if (batteryIcons[i].GetComponent<RectTransform>().anchoredPosition.x > maxX)
            {
                maxX = batteryIcons[i].GetComponent<RectTransform>().anchoredPosition.x;
            }
        }

        float x2 = (maxX - minX) + batteryIcons[0].GetComponent<RectTransform>().sizeDelta.x / 2;
        for (int i = 0; i < batteryIcons.Count; i++)
        {
            batteryIcons[i].GetComponent<RectTransform>().anchoredPosition = new Vector2(batteryIcons[i].GetComponent<RectTransform>().anchoredPosition.x - x2, batteryIcons[i].GetComponent<RectTransform>().anchoredPosition.y + batteryIcons[i].GetComponent<RectTransform>().sizeDelta.y / 2f);
        }

        maxBatteryTime = rd.batteryDuration * batteryIcons.Count;
        startTime = Time.time;
    }

    // Update is called once per frame
    private void Update()
    {
        float perc = 1 - Mathf.InverseLerp(startTime, startTime + maxBatteryTime, (Time.time - startTime));
        int startI = (int)Mathf.Round(batteryIcons.Count - (batteryIcons.Count * perc));
        float dPerc = 1f / batteryIcons.Count;
        for (int i = startI - 1; i >= 0; i--)
        {
            float posPerc = (batteryIcons.Count - i) * dPerc;
            float pDiff = posPerc - perc;
            float nPerc = 1 - pDiff / dPerc;
            if (nPerc <= 0)
            {
                batteryIcons[i].GetComponent<Image>().color = cols[3];
            }
            else if (nPerc > .25f)
            {
                batteryIcons[i].GetComponent<Image>().color = cols[1];
            }
            else
            {
                batteryIcons[i].GetComponent<Image>().color = cols[2];
            }
        }
    }
}