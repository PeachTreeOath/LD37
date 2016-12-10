using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Logo : MonoBehaviour {

	Vector3 [] tarPos;
	Vector3 [] startPos;
	float [] moveSpeeds = {.4f, 1.5f};
	float startTime;
	int curIdx;

	float delayTimer;
	float delayTimeout = 1f;
	bool resetTime;

	// Use this for initialization
	void Start () {
		resetTime = false;
		delayTimer = Time.time - delayTimeout;
		tarPos = new Vector3[2];
		startPos = new Vector3[2];
		curIdx = 0;
		startTime = Time.time;
		tarPos[1] = gameObject.transform.position;
		startPos[1] = gameObject.transform.position + Vector3.down * 240;

		tarPos[0] = startPos[1];
		gameObject.transform.position += Vector3.down * 600;
		startPos[0] = gameObject.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		if(!resetTime && 
			(Time.time - startTime) * moveSpeeds[curIdx] >= 1 &&
			curIdx < tarPos.Length - 1)
		{
			curIdx++;
			delayTimer = Time.time;
			resetTime = true;
		}

		if(Time.time - delayTimer > delayTimeout)
		{
			if(resetTime)
			{
				resetTime = false;
				startTime = Time.time;
			}
			gameObject.transform.position = Vector3.Lerp(startPos[curIdx], tarPos[curIdx], (Time.time - startTime) * moveSpeeds[curIdx]);
		}
	}
}
