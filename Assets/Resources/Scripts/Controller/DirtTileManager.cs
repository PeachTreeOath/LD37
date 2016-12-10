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
        // Decrement the opacity value by the amount afforded in the upgrade manager.
        opacityPercentage = opacityPercentage - (UpgradeManager.instance.deepCleanLevel / 5);
        UpdateOpacity();
    }

    // Update is called once per frame
    private void Update()
    {
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