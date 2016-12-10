﻿using UnityEngine;

public class DirtTileManager : MonoBehaviour
{
    /// <summary>
    /// Current color value shown in the sprite renderer.
    /// </summary>
    public Color curColor;

    /// <summary>
    /// The current opacity value of the tile, as a percentage.
    /// </summary>
    public float opacityPercentage;
    private float t = 0;
    private float fadeDuration = 3.0f; // seconds

    // Use this for initialization
    private void Start() {
        curColor = GetComponent<SpriteRenderer>().color;
        curColor.a = 1;
        opacityPercentage = 0.7f;
        UpdateOpacity();
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Player")) {

            // start lerp control value when roomba enters dirt
            t = 0;

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
            }
            UpdateOpacity();
        }
    }

    public void Update() {
        Color oldColor = GetComponent<SpriteRenderer>().color;

        GetComponent<SpriteRenderer>().color = Color.Lerp(oldColor, curColor, t);

        // Increment lerp control value until maximum over fade duration
        if (t < 1) {
            t += Time.deltaTime / fadeDuration;
        }
    }

    /// <summary>
    /// Update the opacity of the sprite renderer to reflect the current opacity
    /// percentage value.
    /// </summary>
    private void UpdateOpacity() {
        curColor = new Color(curColor.r, curColor.g, curColor.b, curColor.a * opacityPercentage);
    }
}