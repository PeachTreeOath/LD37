using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeManager : MonoBehaviour {

	public enum UpgradeEnum {THORNS, VISION, ENERGY, DEEP_CLEAN, CLEAN_RADIUS, TURN_RADIUS};

	static Dictionary<UpgradeEnum, Upgrade> upgrades;

    public static int money = 200;

	static UpgradeManager instance;

    public static UpgradeManager Instance
	{
		get
		{
			if(instance == null)
			{
				GameObject foo = new GameObject();
				foo.name = "UpgradeManager";
				instance = foo.AddComponent<UpgradeManager>();
				upgrades = new Dictionary<UpgradeEnum, Upgrade>();
				DontDestroyOnLoad(foo);
			}

			return instance;
		}
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
		Debug.Log(upgrades.Count + "");
		foreach(Upgrade u in upgrades.Values)
		{
			Debug.Log(u.upgradeType + " " + u.value);
			if(u.cb != null && u.cb.Length > 0)
			{
				gameObject.SendMessage(u.cb, SendMessageOptions.DontRequireReceiver);
			}
		}
	}
}
