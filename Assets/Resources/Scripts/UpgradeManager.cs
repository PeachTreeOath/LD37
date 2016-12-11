using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeManager : MonoBehaviour {

	public enum UpgradeEnum {THORNS, VISION, ENERGY, DEEP_CLEAN, CLEAN_RADIUS, TURN_RADIUS, DURABILITY, SPEED};

	static Dictionary<UpgradeEnum, Upgrade> upgrades;

    public static int money = 200;

	static UpgradeManager instance;

	GameObject thornsFab;
	GameObject thornsObj;
	GameObject player;

    public static UpgradeManager Instance
	{
		get
		{
			if(instance == null)
			{
				GameObject foo = new GameObject();
				foo.name = "UpgradeManager";
				instance = foo.AddComponent<UpgradeManager>();
				InitUpgrades();
				DontDestroyOnLoad(foo);
			}

			return instance;
		}
	}

	static void InitUpgrades()
	{
		upgrades = new Dictionary<UpgradeEnum, Upgrade>();

		int [] values = {0, 0, 0, 0, 0, 0, 0, 0};
		int [] maxValues = {5, 5, 5, 5, 5, 5, 5, 5};
		bool [] cbs = {true, false, false, false, false, false, false, false};
		string [] desc = {"1", "2", "3", "4", "5", "6", "7", "8"};
		int [] costs = {200, 200, 200, 200, 200, 200, 200, 200};
		UpgradeEnum [] types = {UpgradeEnum.THORNS, UpgradeEnum.VISION, UpgradeEnum.ENERGY, UpgradeEnum.DEEP_CLEAN, UpgradeEnum.CLEAN_RADIUS, UpgradeEnum.TURN_RADIUS, UpgradeEnum.DURABILITY, UpgradeEnum.SPEED};

		for(int i = 0; i < values.Length; i++)
		{
			Upgrade u = ScriptableObject.CreateInstance<Upgrade>();
			u.upgradeType = types[i];
			u.value = values[i];
			u.maxValue = maxValues[i];
			u.cost = costs[i];
			u.description = desc[i];
			u.cb = ((cbs[i])?types[i].ToString().ToLower():null);
			upgrades.Add(types[i], u);
		}
	}

	public Upgrade GetUpgradeInfo(UpgradeEnum t)
	{
		Upgrade u = null;
		if(upgrades.ContainsKey(t))
		{
			u = upgrades[t];
		}

		return u;
	}

	public void AddUpgrade(UpgradeEnum t, bool hasCB)
	{
		if(upgrades.ContainsKey(t))
		{
			upgrades[t].value++;
		}/*else
		{
			Upgrade u = ScriptableObject.CreateInstance<Upgrade>();
			u.upgradeType = t;
			u.value = 1;
			if(hasCB)
			{
				u.cb = t.ToString().ToLower();
			}
			upgrades.Add(t, u);
		}*/
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

	public bool Downgrade(UpgradeEnum t)
	{
		bool ret = false;
		Upgrade u;
		if(upgrades.TryGetValue(t, out u))
		{
			Debug.Log(Time.time + " Downgrading " + u.upgradeType.ToString() + " val " + u.value);
			u.value--;
			if(u.value <= 0)
			{
				u.value = 0;
				//upgrades.Remove(t);
				ret = true;
			}
		}

		return ret;
	}
	
	// Update is called once per frame
	void Update () {
		foreach(Upgrade u in upgrades.Values)
		{
			if(u.cb != null && u.cb.Length > 0 && u.value > 0)
			{
				gameObject.SendMessage(u.cb, SendMessageOptions.DontRequireReceiver);
			}
		}
	}

	void thorns()
	{
		if(thornsObj == null)
		{
			thornsObj = Instantiate(Resources.Load("Prefabs/Thorns")) as GameObject;
		}
		if(player == null)
		{
			player = GameObject.Find("RoombaUnit");
		}
		if(player != null)
		{
			thornsObj.transform.position = player.transform.position;
		}
	}
}
