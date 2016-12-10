using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrontLightController : MonoBehaviour {

    const float lightDistanceX = 0.15f;
    const float lightDistanceY = 0.01f;
    const float offset = 0.1f;

    // Use this for initialization
    void Start () {
        
    }
    
    // Update is called once per frame
    void Update () {
 
        transform.position = transform.parent.position + transform.up * -(Mathf.Cos(transform.rotation.w)*lightDistanceX + lightDistanceY);
    }
}