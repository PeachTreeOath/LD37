﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoombaController : MonoBehaviour {

    private Rigidbody2D rb;
    public float MovementSpeed;
    public float RotationSpeed;

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {

    }

    void FixedUpdate() {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        Vector2 moveForward = new Vector2(0, MovementSpeed * moveVertical);
        rb.AddForce(transform.up * MovementSpeed * moveVertical);
        rb.MoveRotation(rb.rotation - moveHorizontal * RotationSpeed);
    }
}