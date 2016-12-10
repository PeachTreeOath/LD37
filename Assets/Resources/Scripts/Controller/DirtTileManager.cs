using UnityEngine;

public class DirtTileManager : MonoBehaviour
{
    /// <summary>
    /// Current color value shown in the sprite renderer.
    /// </summary>
    public Color curColor;

    /// <summary>
    /// The current opacity value of the tile, as a percentage.
    /// </summary>
    public float opacityPercentage = 0.7f;

    // Use this for initialization
    private void Start()
    {
        curColor = GetComponent<SpriteRenderer>().color;
        UpdateOpacity();
    }

    private void OnTriggerEnter(Collider other)
    {
        int curUpgrade = UpgradeManager.instance.GetUpgradeValue(UpgradeManager.UpgradeEnum.DEEP_CLEAN) + 1;

        // Max Deep Clean upgrade level is 5.
        if (curUpgrade > 5)
            curUpgrade = 5;

        // Decrement the opacity value by the amount afforded by the current
        // upgrade level. Level 0 decrements by 1/5, Level 1 by 2/5, etc
        opacityPercentage = opacityPercentage - (curUpgrade / 5);
        UpdateOpacity();
    }

    /// <summary>
    /// Update the opacity of the sprite renderer to reflect the current opacity
    /// percentage value.
    /// </summary>
    private void UpdateOpacity()
    {
        curColor = new Color(curColor.r, curColor.g, curColor.b, 255 * opacityPercentage);
    }
}