using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : Singleton<AudioManager> {

    private AudioSource musicChannel;
    private AudioSource soundChannel;

    // Use this for initialization
    void Start () {
        musicChannel = gameObject.AddComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
