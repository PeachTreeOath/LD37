using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flash : MonoBehaviour {

	public int numFlashes;

	float flashSpeed = 5.15f;
	int count;
	float timer;
	int curIdx;
	SpriteRenderer rend;
	float [] targets = {1, 0};

	// Use this for initialization
	void Start () {
		curIdx = 0;
		count = 0;
		timer = MyTime.Instance.time;
		rend = GetComponent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
		float prog = (MyTime.Instance.time - timer) * flashSpeed;

		Color col = rend.color;

		if(count != numFlashes)
		{
			float tarA = Mathf.Lerp(targets[curIdx], targets[(curIdx+1)&0x1], prog);
			col.a = tarA;
		}else
		{
			col.a = 1;
			Destroy(this);
		}

		rend.color = col;

		if(prog >= 1)
		{
			timer = MyTime.Instance.time;
			curIdx ^= 1;
			if(curIdx % 2 == 0)
			{
				count++;
			}
		}
	}
}
