using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeManager : MonoBehaviour
{
    public enum UpgradeEnum { THORNS, VISION, ENERGY, DEEP_CLEAN, CLEAN_RADIUS, TURN_RADIUS, DURABILITY, SPEED };

    private static Dictionary<UpgradeEnum, Upgrade> upgrades;

    public static int money = 0;

    private static UpgradeManager instance;

    private GameObject thornsFab;
    private GameObject thornsObj;
    private GameObject player;

    public static UpgradeManager Instance
    {
        get
        {
            if (instance == null)
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

    private static void InitUpgrades()
    {
        upgrades = new Dictionary<UpgradeEnum, Upgrade>();

        int[] values = { 0, 0, 0, 0, 0, 0, 0, 0 };
        int[] maxValues = { 5, 5, 5, 5, 5, 5, 5, 5 };
        bool[] cbs = { true, false, false, false, false, false, false, false };
        string[] desc = {"Thorns allows you to chase off the pesky Mittens and pals. Try running into the dastardly feline and enjoy the newly colored ring. More Upgrades means more Thorns!",
            "Max Vision increases how much of The Room you can see any at time. Mo Upgrades Mo Vision.",
            "Battery Life increases the how long Roomba can be out on a single run.",
            "Deeper Clean: Each upgrade allows you to pull in more dirt from a single carpet tile.",
            "Clean Radius allows Roomba to have a larger radius to suck in dirt.",
            "Turning: increases how quickly you can turn Roomba.",
            "Durability: increases how many hits you can take from the environment.",
            "Speed: Increases how fast Roomba can move."};
        int[] costs = { 200, 200, 200, 200, 200, 200, 200, 200 };
        UpgradeEnum[] types = { UpgradeEnum.THORNS, UpgradeEnum.VISION, UpgradeEnum.ENERGY, UpgradeEnum.DEEP_CLEAN, UpgradeEnum.CLEAN_RADIUS, UpgradeEnum.TURN_RADIUS, UpgradeEnum.DURABILITY, UpgradeEnum.SPEED };

        for (int i = 0; i < values.Length; i++)
        {
            Upgrade u = ScriptableObject.CreateInstance<Upgrade>();
            u.upgradeType = types[i];
            u.value = values[i];
            u.maxValue = maxValues[i];
            u.cost = costs[i];
            u.description = desc[i];
            u.cb = ((cbs[i]) ? types[i].ToString().ToLower() : null);
            upgrades.Add(types[i], u);
        }
    }

    public Upgrade GetUpgradeInfo(UpgradeEnum t)
    {
        Upgrade u = null;
        if (upgrades.ContainsKey(t))
        {
            u = upgrades[t];
        }

        return u;
    }

    public void AddUpgrade(UpgradeEnum t, bool hasCB)
    {
        if (upgrades.ContainsKey(t))
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
        if (upgrades.TryGetValue(t, out u))
        {
            ret = u.value;
        }
        return ret;
    }

    public bool Downgrade(UpgradeEnum t)
    {
        bool ret = false;
        Upgrade u;
        if (upgrades.TryGetValue(t, out u))
        {
            Debug.Log(MyTime.Instance.time + " Downgrading " + u.upgradeType.ToString() + " val " + u.value);
            u.value--;
            if (u.value <= 0)
            {
                u.value = 0;
                //upgrades.Remove(t);
                ret = true;
            }
        }

        return ret;
    }

    // Update is called once per frame
    private void Update()
    {
        foreach (Upgrade u in upgrades.Values)
        {
            if (u.cb != null && u.cb.Length > 0 && u.value > 0)
            {
                gameObject.SendMessage(u.cb, SendMessageOptions.DontRequireReceiver);
            }
        }
    }

    private void thorns()
    {
        if (thornsObj == null)
        {
            thornsObj = Instantiate(Resources.Load("Prefabs/Thorns")) as GameObject;
        }
        if (player == null)
        {
            player = GameObject.Find("RoombaUnit");
        }
        if (player != null)
        {
            thornsObj.transform.position = player.transform.position;
        }
    }
}