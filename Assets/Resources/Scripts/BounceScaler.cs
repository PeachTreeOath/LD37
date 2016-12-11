using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BounceScaler : MonoBehaviour {

	public float mult;
	public float speed;

	Vector3[] scales;
	float timer;

	// Use this for initialization
	void Start () {
		Debug.Log(MyTime.Instance.time + " bounce");
		scales = new Vector3[2];
		scales[0] = gameObject.transform.localScale;
		scales[1] = scales[0] * mult;
		timer = MyTime.Instance.time;
	}
	
	// Update is called once per frame
	void Update () {
		float p = (MyTime.Instance.time - timer) * speed;
		if(p >= 1)
		{
            AudioManager.instance.PlaySound("Low_Battery", .45f);
			Vector3 tmp = scales[0];
			scales[0] = scales[1];
			scales[1] = tmp;
			timer = MyTime.Instance.time;
			gameObject.transform.localScale = scales[0];
			p = 0;
		}

		gameObject.transform.localScale = Vector3.Lerp(scales[0], scales[1], p);
	}
}
