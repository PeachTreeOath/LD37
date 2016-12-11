using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatController : MonoBehaviour
{
    
    void OnCollisionEnter2D(Collision2D col)
    {
        AudioManager.instance.PlaySound("kitty");
        AudioManager.instance.PlaySound("Bounce");
    }
}
