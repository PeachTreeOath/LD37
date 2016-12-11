using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCameraController : MonoBehaviour {

    public GameObject player;
    private Vector3 offset;
	float startSize;
	Camera cam;

	// Use this for initialization
	void Start () {
        // Get distance from roomba starting position
        offset = transform.position - player.transform.position;
		cam = GetComponent<Camera>();
		startSize = cam.orthographicSize;
	}
	
	// Update is called once per frame
	void Update () {
        transform.position = player.transform.position + offset;

		cam.orthographicSize = startSize + UpgradeManager.Instance.GetUpgradeValue(UpgradeManager.UpgradeEnum.VISION) * .25f;
	}
}
