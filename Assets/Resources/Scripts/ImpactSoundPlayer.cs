using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImpactSoundPlayer : MonoBehaviour {

    public string[] soundsToPlay;

    void OnCollisionEnter2D(Collision2D col)
    {
        foreach(string clipName in soundsToPlay)
        {
            AudioManager.instance.PlaySound(clipName);
        }
    }

}
