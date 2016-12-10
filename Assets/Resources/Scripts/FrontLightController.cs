using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrontLightController : MonoBehaviour {

    const float lightDistance = 0.15f;
    const float offset = 0.01f;

    // Use this for initialization
    void Start () {
        
    }
    
    // Update is called once per frame
    void Update () {

        // Elliptical orbit
        transform.position = transform.parent.position + transform.up * -(Mathf.Cos(transform.rotation.w) * lightDistance + offset);
    }
}