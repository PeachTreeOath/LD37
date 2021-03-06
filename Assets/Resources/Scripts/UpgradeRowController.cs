﻿using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/// <summary>
/// Tracks a "row group" in the upgrades panel.
/// </summary>
public class UpgradeRowController : MonoBehaviour
{
    /// <summary>
    /// Reference to the text which displays the current level of the upgrade.
    /// </summary>
    public Text currLevel;

    /// <summary>
    /// Reference to the text which displays the current cost of the upgrade.
    /// </summary>
    public Text currCost;

    /// <summary>
    /// Reference to the description text box.
    /// </summary>
    public Text description;

    /// <summary>
    /// The upgrade
    /// </summary>
    public Button buyButton;

    /// <summary>
    /// Setting for the type upgrade managed by this panel
    /// </summary>
    public UpgradeManager.UpgradeEnum type;

    /// <summary>
    /// The upgrade type displayed in this row, as retrieved from the UpgradeManager.
    /// </summary>
    private Upgrade upgradeObj;

    private HoverHandler buttonHover;

	GameObject dirCounter;

    // Use this for initialization
    public void Start()
    {
		dirCounter = GameObject.Find("DirtCounter");
        if (currLevel == null)
            Debug.LogWarning("Current level text not assigned.");
        upgradeObj = UpgradeManager.Instance.GetUpgradeInfo(type);

        if (upgradeObj != null)
        {
            currLevel.text = upgradeObj.baseValue.ToString();
            currCost.text = (upgradeObj.cost + (int)(upgradeObj.cost * upgradeObj.baseValue * .33f)).ToString();
        }
        buyButton = GetComponentInChildren<Button>();
        if (buyButton != null)
        {
            buyButton.onClick.AddListener(delegate { ButtonPress(); });
            buttonHover = buyButton.gameObject.AddComponent<HoverHandler>();
        }
    }

    /// <summary>
    /// Handle button press
    /// </summary>
    public void ButtonPress()
    {
        if (UpgradeManager.money >= (upgradeObj.cost + (int)(upgradeObj.cost * upgradeObj.baseValue * .33f)) &&
            upgradeObj.baseValue < upgradeObj.maxValue)
        {
            UpgradeManager.money -= (upgradeObj.cost + (int)(upgradeObj.cost * upgradeObj.baseValue * .33f));
			dirCounter.GetComponent<Text>().text = UpgradeManager.money+"";
            UpgradeManager.Instance.AddUpgrade(type, ((upgradeObj.cb != null) ? true : false));
            AudioManager.instance.PlaySound("Money_Buy");
            Referesh();
        }
        else
        {
            AudioManager.instance.PlaySound("Money_Invalid");
        }
        if (upgradeObj.baseValue == upgradeObj.maxValue)
        {
            buyButton.enabled = false;
        }
    }

    /// <summary>
    /// Refreshes displayed information.
    /// </summary>
    private void Referesh()
    {
        currLevel.text = upgradeObj.baseValue.ToString();
        currCost.text = (upgradeObj.cost + (int)(upgradeObj.cost * upgradeObj.baseValue * .33f)).ToString();
    }

    // Update is called once per frame
    private void Update()
    {
        if (buttonHover.isHovered)
            description.text = upgradeObj.description;

		if(upgradeObj.baseValue == upgradeObj.maxValue)
		{
			buyButton.gameObject.transform.GetChild(0).GetComponent<Text>().text = "MAXED";
		}
    }
}