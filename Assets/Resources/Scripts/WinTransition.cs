using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinTransition : MonoBehaviour {

	GameObject winScreenFab;
	GameObject winTextObj;

	float timer;
	float moveSpeed;

	Vector3 startPos;
	Vector3 tarPos;
	GameObject canvas;

	// Use this for initialization
	void Start () {
		canvas = GameObject.Find("HudCanvas");
		winTextObj = Instantiate(Resources.Load("Prefabs/WinText")) as GameObject;
		winScreenFab = Resources.Load("Prefabs/WinScreen") as GameObject;
		winTextObj.transform.SetParent(canvas.transform);
		RectTransform c = canvas.GetComponent<RectTransform>();
		startPos = new Vector2(c.sizeDelta.x/2, c.sizeDelta.y/2) - Vector2.right * 800;
		tarPos = new Vector2(c.sizeDelta.x/2, c.sizeDelta.y/2);
		timer = Time.time;
		moveSpeed = .5f;
	}
	
	// Update is called once per frame
	void Update () {
		Debug.Log("timeScale " + MyTime.Instance.timeScale);
		winTextObj.transform.position = Vector3.Lerp(startPos, tarPos, (Time.time - timer) * moveSpeed);
		if((Time.time - timer) * moveSpeed > 1.5f)
		{
			Debug.Log(Time.time + " win");
			GameObject foo = Instantiate(winScreenFab) as GameObject;
			foo.transform.position = Vector3.zero;
			Destroy(this);
		}
	}
}
