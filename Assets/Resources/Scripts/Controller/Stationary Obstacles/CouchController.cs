using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CouchController : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            AudioManager.instance.PlaySound("collision_sofa");
        }
    }
}