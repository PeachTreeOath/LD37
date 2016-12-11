using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeManager : Singleton<UpgradeManager>
{
    public enum UpgradeEnum { THORNS, VISION, ENERGY, DEEP_CLEAN, CLEAN_RADIUS, TURN_RADIUS, DURABILITY, SPEED };

    private static Dictionary<UpgradeEnum, Upgrade> upgrades = new Dictionary<UpgradeEnum, Upgrade>();

    public int money = 200;

    private GameObject thornsFab;
    private GameObject thornsObj;
    private GameObject player;

    /// <summary>
    /// Initialize the upgrade manager
    /// </summary>
    protected override void Awake()
    {
        base.Awake();
        upgrades.Add(UpgradeEnum.THORNS, new Upgrade(UpgradeEnum.THORNS, 5, 200, "Make more spikey"));
        upgrades.Add(UpgradeEnum.VISION, new Upgrade(UpgradeEnum.VISION, 5, 200, "Improve viewing distance"));
        upgrades.Add(UpgradeEnum.ENERGY, new Upgrade(UpgradeEnum.ENERGY, 5, 200, "Longer battery life"));
        upgrades.Add(UpgradeEnum.DEEP_CLEAN, new Upgrade(UpgradeEnum.DEEP_CLEAN, 5, 200, "Stronger cleaning power"));
        upgrades.Add(UpgradeEnum.CLEAN_RADIUS, new Upgrade(UpgradeEnum.CLEAN_RADIUS, 5, 200, "Wider cleaning radius"));
        upgrades.Add(UpgradeEnum.SPEED, new Upgrade(UpgradeEnum.SPEED, 5, 200, "Faster move speed"));
        upgrades.Add(UpgradeEnum.TURN_RADIUS, new Upgrade(UpgradeEnum.TURN_RADIUS, 5, 200, "Improved turn radius."));
    }

    /// <summary>
    /// If an upgrade type is found in the manager, increment it's current value.
    /// </summary>
    /// <param name="upgrade"></param>
    public void IncrementUpgrade(UpgradeEnum upgrade)
    {
        if (upgrades.ContainsKey(upgrade))
        {
            upgrades[upgrade].level++;
        }
    }

    /// <summary>
    /// Retrieve the specified upgrade container from the UpgradeManager.
    /// </summary>
    /// <param name="upgrade"></param>
    /// <returns></returns>
    public Upgrade GetUpgrade(UpgradeEnum upgrade)
    {
        if (upgrades.ContainsKey(upgrade))
        {
            return upgrades[upgrade];
        }
        else
        {
            return null;
        }
    }

    /// <summary>
    /// ???
    /// </summary>
    /// <param name="upgradeType"></param>
    /// <param name="hasCB"></param>
    public void AddUpgradeAndIncrement(UpgradeEnum upgradeType, bool hasCB)
    {
        if (upgrades.ContainsKey(upgradeType))
        {
            upgrades[upgradeType].level++;
        }
        else
        {
            //Upgrade u = ScriptableObject.CreateInstance<Upgrade>();
            //u.upgradeType = upgradeType;
            //u.value = 1;
            //if (hasCB)
            //{
            //    u.cb = upgradeType.ToString().ToLower();
            //}
            //upgrades.Add(upgradeType, u);
        }
    }

    /// <summary>
    /// Retrieve the upgrade level of the provided type.
    /// </summary>
    /// <param name="t"></param>
    /// <returns></returns>
    public int GetUpgradeLevel(UpgradeEnum t)
    {
        int ret = 0;
        Upgrade u;
        if (upgrades.TryGetValue(t, out u))
        {
            ret = u.level;
        }
        return ret;
    }

    /// <summary>
    /// ???
    /// </summary>
    /// <param name="t"></param>
    /// <returns></returns>
    public bool Downgrade(UpgradeEnum t)
    {
        bool ret = false;
        Upgrade u;
        if (upgrades.TryGetValue(t, out u))
        {
            u.level--;
            if (u.level <= 0)
            {
                upgrades.Remove(t);
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
            if (u.cb != null && u.cb.Length > 0)
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