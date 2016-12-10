using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCameraController : MonoBehaviour {

    public GameObject player;
    private Vector3 offset;

	// Use this for initialization
	void Start () {
        // Get distance from roomba starting position
        offset = transform.position - player.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
        transform.position = player.transform.position + offset;
	}
}
