using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImpactSoundPlayer : MonoBehaviour
{

    public string[] soundsToPlay;

    private GameObject roomba;
    private float distance = 3f;
    private float currDistance;
    private float camSize = 1;

    void Start()
    {
        roomba = GameObject.Find("RoombaUnit");
        currDistance = distance;
    }

    void Update()
    {
        if(Camera.main.orthographicSize != camSize)
        {
            camSize = Camera.main.orthographicSize;
            currDistance = distance * camSize;
            Debug.Log(currDistance);
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (Vector2.Distance(col.contacts[0].point, (Vector2)roomba.transform.position) < currDistance)
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
