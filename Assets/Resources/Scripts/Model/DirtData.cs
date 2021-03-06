﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirtData : MonoBehaviour {

	public int value;
	public int baseHealth;
	public float multFactor = 1;
	[HideInInspector]
	public int health;
	[HideInInspector]
	public int collected;

	void Start()
	{
		health = baseHealth;
        DirtManager.instance.IncreaseDirtValue(baseHealth);
    }
}
