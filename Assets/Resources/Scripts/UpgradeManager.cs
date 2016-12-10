using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeManager : MonoBehaviour {

	public enum UpgradeEnum {THORNS, VISION, ENERGY, DEEP_CLEAN, CLEAN_RADIUS, TURN_RADIUS};

	Dictionary<UpgradeEnum, Upgrade> upgrades;

    public int money = 200;

    public static UpgradeManager Instance = null;

	void Start()
	{
		upgrades = new Dictionary<UpgradeEnum, Upgrade>();
		Instance = this;
	}

	public void AddUpgrade(UpgradeEnum t, bool hasCB)
	{
		if(upgrades.ContainsKey(t))
		{
			upgrades[t].value++;
		}else
		{
			Upgrade u = ScriptableObject.CreateInstance<Upgrade>();
			u.upgradeType = t;
			u.value = 1;
			if(hasCB)
			{
				u.cb = t.ToString().ToLower();
			}
			upgrades.Add(t, u);
		}
	}

	public int GetUpgradeValue(UpgradeEnum t)
	{
		int ret = 0;
		Upgrade u;
		if(upgrades.TryGetValue(t, out u))
		{
			ret = u.value;
		}
		return ret;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
