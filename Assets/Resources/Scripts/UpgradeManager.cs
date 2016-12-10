using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeManager : MonoBehaviour {

	public enum UpgradeEnum {THORNS, VISION, ENERGY, DEEP_CLEAN, CLEAN_RADIUS, TURN_RADIUS, DURABILITY, SPEED};

	Dictionary<UpgradeEnum, Upgrade> upgrades;

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
				instance.Start();
				DontDestroyOnLoad(foo);
			}

			return instance;
		}
	}

	public void Start()
	{
		upgrades = new Dictionary<UpgradeEnum, Upgrade>();
		if(instance == null)
		{
			instance = this;
			DontDestroyOnLoad(gameObject);
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
		foreach(Upgrade u in upgrades.Values)
		{
			if(u.cb != null && u.cb.Length > 0)
			{
				gameObject.SendMessage(u.cb, SendMessageOptions.DontRequireReceiver);
			}
		}
	}
}
