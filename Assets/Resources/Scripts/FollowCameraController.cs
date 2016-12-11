﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCameraController : MonoBehaviour
{
    public GameObject player;
    private Vector3 offset;
    private float startSize;
    private Camera cam;

    // Use this for initialization
    private void Start()
    {
        // Get distance from roomba starting position
        offset = transform.position - player.transform.position;
        cam = GetComponent<Camera>();
        startSize = cam.orthographicSize;
    }

    // Update is called once per frame
    private void Update()
    {
        transform.position = player.transform.position + offset;

        cam.orthographicSize = startSize + UpgradeManager.instance.GetUpgradeLevel(UpgradeManager.UpgradeEnum.VISION) * .25f;
    }
}