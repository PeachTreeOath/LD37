using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirtData : MonoBehaviour {

	public int baseValue;
	public float multFactor = 1;
	[HideInInspector]
	public int value;

	void Start()
	{
		value = baseValue;
	}
}
