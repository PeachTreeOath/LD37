using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyTime : MonoBehaviour {

	[HideInInspector]
	public float deltaTime;
	[HideInInspector]
	public float timeScale;
	public float time;

	static MyTime instance;

	public static MyTime Instance
	{
		get
		{
			if(instance == null)
			{
				GameObject foo = new GameObject();
				foo.name = "MyTime";
				instance = foo.AddComponent<MyTime>();
				instance.Awake();
				DontDestroyOnLoad(foo);
			}

			return instance;
		}
	}

	void Awake()
	{
		time = 0;
		deltaTime = Time.deltaTime * timeScale;
	}

	void Start()
	{
		time = 0;
		deltaTime = Time.deltaTime * timeScale;
	}

	// Update is called once per frame
	void Update () {
		deltaTime = Time.deltaTime * timeScale;
		time += deltaTime;
	}
}
