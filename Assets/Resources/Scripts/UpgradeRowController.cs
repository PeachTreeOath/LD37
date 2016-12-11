using System;
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

    // Use this for initialization
    public void Start()
    {
        if (currLevel == null)
            Debug.LogWarning("Current level text not assigned.");
		upgradeObj = UpgradeManager.Instance.GetUpgradeInfo(type);

        if (upgradeObj != null)
        {
			currLevel.text = upgradeObj.value.ToString();
			currCost.text = (upgradeObj.cost + (int)(upgradeObj.cost * upgradeObj.value * .33f)).ToString();
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
		if (UpgradeManager.money >= (upgradeObj.cost + (int)(upgradeObj.cost * upgradeObj.value * .33f)) &&
			upgradeObj.value < upgradeObj.maxValue)
        {
			UpgradeManager.money -= (upgradeObj.cost + (int)(upgradeObj.cost * upgradeObj.value * .33f));
			UpgradeManager.Instance.AddUpgrade(type, ((upgradeObj.cb != null)?true:false));
            AudioManager.instance.PlaySound("Money_Buy");
            Referesh();
        }
        else
        {
            AudioManager.instance.PlaySound("Money_Invalid");
        }
		if (upgradeObj.value == upgradeObj.maxValue)
        {
            buyButton.enabled = false;
        }
    }

    /// <summary>
    /// Refreshes displayed information.
    /// </summary>
    private void Referesh()
    {
		currLevel.text = upgradeObj.value.ToString();
		currCost.text = (upgradeObj.cost + (int)(upgradeObj.cost * upgradeObj.value * .33f)).ToString();
    }

    // Update is called once per frame
    private void Update()
    {
        if (buttonHover.isHovered)
            description.text = upgradeObj.description;
    }
}