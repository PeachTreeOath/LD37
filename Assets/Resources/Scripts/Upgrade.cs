using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Upgrade : ScriptableObject {

	public UpgradeManager.UpgradeEnum upgradeType;
	public int baseValue;
	public int value;
	public int maxValue;
	public int cost;
	public string description;
	public string cb;
}
