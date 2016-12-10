using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrontLightController : MonoBehaviour {

    const float lightDistance = 0.15f;
    const float centerOffset = 0.1f;

    // Use this for initialization
    void Start () {
        
    }
    
    // Update is called once per frame
    void Update () {
        transform.position = transform.parent.position + transform.up * lightDistance; 
    }
}
