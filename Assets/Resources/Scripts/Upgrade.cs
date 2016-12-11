using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Represents an upgrade container.
/// </summary>
public class Upgrade
{
    /// <summary>
    /// Enum identifying the type of upgrade.
    /// </summary>
	public UpgradeManager.UpgradeEnum upgradeType;

    /// <summary>
    /// The current upgrade value, from zero to maxVal.
    /// </summary>
    public int level;

    /// <summary>
    /// The max upgrade value available.
    /// </summary>
    public int maxLevel;

    /// <summary>
    /// The cost to purchace this upgrade.
    /// </summary>
    public int costVal;

    /// <summary>
    /// Description text shown when this upgrade is hovered.
    /// </summary>
    public string description;

    /// <summary>
    /// What is this??
    /// </summary>
    public string cb;

    public Upgrade(UpgradeManager.UpgradeEnum upgradeType, int maxVal, int costVal, string description, int level = 0)
    {
        this.upgradeType = upgradeType;
        this.level = level;
        this.maxLevel = maxVal;
        this.costVal = costVal;
        this.description = description;
    }
}