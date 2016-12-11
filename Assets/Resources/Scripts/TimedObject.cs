using UnityEngine;
using System.Collections;

public class TimedObject : MonoBehaviour {
	
	public float lifetime;
	float lifetimer;
	
	// Use this for initialization
	void Start () {
		lifetimer = Time.time;
	}
	
	// Update is called once per frame
	void Update () {
		if(Time.time - lifetimer >= lifetime)
		{
			Destroy(gameObject);
		}
	}
}
