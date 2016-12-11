using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrontLightController : MonoBehaviour {

    const float lightDistance = 0.15f;
    const float offset = 0.01f;
    const float alphaHigh = 1.0f;
    const float alphaLow = 0.5f;
    const float duration = 0.5f;
    float colorControl = 0f;
    bool colorUp;

    // Use this for initialization
    void Start () {
        colorUp = true;
    }
    
    // Update is called once per frame
    void Update () {
        Color colorLow = GetComponent<SpriteRenderer>().color;
        Color colorHigh = GetComponent<SpriteRenderer>().color;
        colorLow.a = alphaLow;
        colorHigh.a = alphaHigh;
        if (colorUp) {
            GetComponent<SpriteRenderer>().color = Color.Lerp(colorLow, colorHigh, colorControl);
        } else {
            GetComponent<SpriteRenderer>().color = Color.Lerp(colorHigh, colorLow, colorControl);
        }

        if (colorControl < 1) {
            colorControl += Time.deltaTime / duration;
        } else {
            colorControl = 0;
            colorUp = !colorUp;
        }
        // Elliptical orbit
        transform.position = transform.parent.position + transform.up * -(Mathf.Cos(transform.rotation.w) * lightDistance + offset);
    }
}