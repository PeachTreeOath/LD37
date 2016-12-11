﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{

	// Use this for initialization
	void Start () {
        AudioManager audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
        audioManager.PlayRoomMusic(.25f);
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
