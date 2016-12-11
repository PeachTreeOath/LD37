using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SofaSoundPlayer : MonoBehaviour {

    void OnCollisionEnter2D(Collision2D col)
    {
        AudioManager.instance.PlaySound("collision_sofa");
        AudioManager.instance.PlaySound("Bounce");
    }
}