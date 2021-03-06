﻿using UnityEngine;
using UnityEngine.UI;

public class DirtTileManager : MonoBehaviour
{
    /// <summary>
    /// Current color value shown in the sprite renderer.
    /// </summary>
    public Color curColor;

    private static GameObject moneyFab;
    private static Text dirtCounter;
    private static float moneyTimer;
    private static float moneyTimeout = .3f;

    /// <summary>
    /// The current opacity value of the tile, as a percentage.
    /// </summary>
    public float opacityPercentage;

    private float dirtTransition = 0;
    private float fadeDuration = 3.0f; // seconds

    private DirtData dirt;
    private bool started;

	private GameObject moneyLossFab;

    // Use this for initialization
    private void Start()
    {
        dirtTransition = 2f;
		moneyLossFab = Resources.Load("Prefabs/MoneyLoss") as GameObject;
		gameObject.tag = "Dirt";
		gameObject.layer = LayerMask.NameToLayer("DirtTile");
        if (dirtCounter == null)
        {
            dirtCounter = GameObject.Find("DirtCounter").GetComponent<Text>();
        }

		moneyTimer = MyTime.Instance.time;
        if (moneyFab == null)
        {
            moneyFab = Resources.Load("Prefabs/Money") as GameObject;
        }
        curColor = GetComponent<SpriteRenderer>().color;
        curColor.a = 1;
        opacityPercentage = 0.7f;
        UpdateOpacity();
        dirt = GetComponent<DirtData>();
        started = true;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        string colliderTag = "";
        if (gameObject.name.Contains("sock")) {
            colliderTag = "Roomba";
        } else {
            colliderTag = "Player";
        }
        
        if (started &&
            other.CompareTag(colliderTag))
        {
            RoombaData rd = GameObject.Find("RoombaUnit").GetComponent<RoombaData>();
            int dmg = (int)((rd.suctionPower + UpgradeManager.Instance.GetUpgradeValue(UpgradeManager.UpgradeEnum.DEEP_CLEAN) * 2) * dirt.multFactor);
            dirt.health -= dmg;
            DirtManager.instance.CalculateDamage(dmg);
            
            opacityPercentage = dirt.health / (float)dirt.baseHealth;

            // start lerp control value when roomba enters dirt
            dirtTransition = 0;
            UpgradeManager.money += dirt.value;
			dirt.collected += dirt.value;
            dirtCounter.text = "" + UpgradeManager.money;

            for (int i = 0; i < 1; i++)
            {
                AudioManager.instance.PlayDirtSound(dirt.collected);
            }

			if (MyTime.Instance.time - moneyTimer > moneyTimeout)
            {
				moneyTimer = MyTime.Instance.time;
                GameObject moneyObj = Instantiate(moneyFab) as GameObject;
                moneyObj.transform.position = other.transform.position;
            }

			if(dirt.value < 0)
			{
				GameObject moneyLossTxt = Instantiate(moneyLossFab) as GameObject;
				moneyLossTxt.GetComponent<Text>().text = "" + dirt.value;
				moneyLossTxt.transform.SetParent(dirtCounter.transform.parent);
				moneyLossTxt.transform.position = dirtCounter.transform.position;
			}

			if (dirt.health <= 0)
			{
				if(dirt.value > 0)
				{
					int totalValue = (int)Mathf.Ceil(dirt.value * (dirt.baseHealth/rd.suctionPower));
					if(dirt.collected < totalValue)
					{
						UpgradeManager.money += totalValue - dirt.collected;
					}
				}
				Destroy(gameObject);
			}
            
            //TODO: show money income
            /*
            int curUpgrade = UpgradeManager.Instance.GetUpgradeValue(UpgradeManager.UpgradeEnum.DEEP_CLEAN) + 1;

            // Max Deep Clean upgrade level is 5.
            if (curUpgrade > 5)
                curUpgrade = 5;

            // Decrement the opacity value by the amount afforded by the current
            // upgrade level. Level 0 decrements by 1/5, Level 1 by 2/5, etc
            opacityPercentage = opacityPercentage - opacityPercentage * (curUpgrade / 5.0f);
            Debug.Log("Opacity " + opacityPercentage.ToString());
            if (opacityPercentage <= 0f) {
                Destroy(this.gameObject);
            }*/
            UpdateOpacity();
        }
    }

    public void Update()
    {
        if (dirtTransition < 1) {
            Color oldColor = GetComponent<SpriteRenderer>().color;

            GetComponent<SpriteRenderer>().color = Color.Lerp(oldColor, curColor, dirtTransition);

            // Increment lerp control value until maximum over fade duration
            dirtTransition += MyTime.Instance.deltaTime / fadeDuration;
        }
    }

    /// <summary>
    /// Update the opacity of the sprite renderer to reflect the current opacity
    /// percentage value.
    /// </summary>
    private void UpdateOpacity()
    {
        curColor = new Color(curColor.r, curColor.g, curColor.b, curColor.a * opacityPercentage);
    }
}