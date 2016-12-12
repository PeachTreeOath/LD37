using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImpactSoundPlayer : MonoBehaviour
{

    public string[] soundsToPlay;

    private GameObject roomba;
    private float distance = 3f;

    void Start()
    {
        roomba = GameObject.Find("RoombaUnit");
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (Vector2.Distance(col.contacts[0].point, (Vector2)roomba.transform.position) < distance)
        {
            PlayImpactSounds();
        }
    }

    void PlayImpactSounds()
    {
        foreach (string clipName in soundsToPlay)
        {
            AudioManager.instance.PlaySound(clipName);
        }
    }

}
