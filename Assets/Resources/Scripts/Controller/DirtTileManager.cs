using UnityEngine;
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

    private float t = 0;
    private float fadeDuration = 3.0f; // seconds

    private DirtData dirt;
    private bool started;

    // Use this for initialization
    private void Start()
    {
        if (dirtCounter == null)
        {
            dirtCounter = GameObject.Find("DirtCounter").GetComponent<Text>();
        }

        moneyTimer = Time.time;
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
        if (started &&
            other.CompareTag("Player"))
        {
            RoombaData rd = other.gameObject.transform.parent.gameObject.GetComponent<RoombaData>();
            int dmg = (int)((rd.suctionPower + UpgradeManager.Instance.GetUpgradeValue(UpgradeManager.UpgradeEnum.DEEP_CLEAN)) * dirt.multFactor);
            dirt.health -= dmg;
            DirtManager.instance.CalculateDamage(dmg);
            
            opacityPercentage = dirt.health / (float)dirt.baseHealth;
            if (dirt.health <= 0)
            {
                Destroy(gameObject);
            }
            else
            {
                // start lerp control value when roomba enters dirt
                t = 0;
                UpgradeManager.money += dirt.value;
                dirtCounter.text = "" + UpgradeManager.money;

                if (Time.time - moneyTimer > moneyTimeout)
                {
                    moneyTimer = Time.time;
                    GameObject moneyObj = Instantiate(moneyFab) as GameObject;
                    moneyObj.transform.position = other.transform.position;
                }
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
        Color oldColor = GetComponent<SpriteRenderer>().color;

        GetComponent<SpriteRenderer>().color = Color.Lerp(oldColor, curColor, t);

        // Increment lerp control value until maximum over fade duration
        if (t < 1)
        {
            t += Time.deltaTime / fadeDuration;
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