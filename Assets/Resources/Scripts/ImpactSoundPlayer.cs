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

	float timer;
	float timeout = 1;

    void Start()
    {
        roomba = GameObject.Find("RoombaUnit");
        currDistance = distance;
		timer = MyTime.Instance.time;
    }

    void Update()
    {
        if(Camera.main.orthographicSize != camSize)
        {
            camSize = Camera.main.orthographicSize;
            currDistance = distance * camSize;
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
		if(MyTime.Instance.time - timer > timeout)
		{
			timer = MyTime.Instance.time;
	        foreach (string clipName in soundsToPlay)
	        {
	            AudioManager.instance.PlaySound(clipName);
	        }
		}
    }

}
