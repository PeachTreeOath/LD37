using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImpactSoundPlayer : MonoBehaviour, ISizeListener
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

        Camera.main.GetComponent<FollowCameraController>().RegisterSizeListener(this);
    }

    public void SizeChanged(float newSize)
    {
        currDistance = distance * newSize;
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (Vector2.Distance(col.contacts[0].point, (Vector2)roomba.transform.position) < currDistance)
        {
            PlayImpactSounds();
        }
    }

    void OnDestroy()
    {
        Camera.main.GetComponent<FollowCameraController>().DeregisterSizeListener(this);
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
