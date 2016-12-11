using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoombaController : MonoBehaviour {
    private Rigidbody2D rb;
    private RoombaData rd;
    public float rotUpgradeMult;

    private Vector3 lastPos;
    private float startTime;

    /// <summary>
    /// Tracks if the roomba is in a reversal state, if it has collided with an object.
    /// </summary>
    private bool isReversing = false;

    /// <summary>
    /// Tracks time duration of a reversal.
    /// </summary>
    private float currReverseTime = 0;

    /// <summary>
    /// Adjusts roomba flyback speed.
    /// </summary>
    private float bounceVelocity = 5;

    /// <summary>
    /// Duration of time in miliseconds to reverse after a collision.
    /// </summary>
    private float reverseDuration = 700;

    private bool isBraking;

    private float dragControl;

    // Use this for initialization
    private void Start() {
        rb = GetComponent<Rigidbody2D>();
        rd = GetComponent<RoombaData>();
        lastPos = gameObject.transform.position;
        startTime = Time.time;
        dragControl = 0;
    }

    // Update is called once per frame
    private void Update() {
        transform.FindChild("RoombaBody").rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, 0, 0);
        isBraking = Input.GetKey(KeyCode.Space) | Input.GetKey(KeyCode.S) | Input.GetKey(KeyCode.DownArrow);
        if (isBraking) {
            rb.drag = Mathf.Lerp(0, 10, dragControl);
            if (dragControl < 1) {
                dragControl += Time.deltaTime / 5f;
            }
        }

    }

    private void FixedUpdate() {
        float moveHorizontal = Input.GetAxis("Horizontal");

        if (isReversing)
            rb.AddForce(transform.up * Mathf.Lerp(rd.maxMoveSpeed + bounceVelocity, rd.minMoveSpeed, (currReverseTime / reverseDuration)));
        else {
            float minSpeed = rd.minMoveSpeed + UpgradeManager.Instance.GetUpgradeValue(UpgradeManager.UpgradeEnum.SPEED) * 5;
            float maxSpeed = rd.maxMoveSpeed + UpgradeManager.Instance.GetUpgradeValue(UpgradeManager.UpgradeEnum.SPEED) * 2;
            float force = -Mathf.Lerp(minSpeed, maxSpeed, (Time.time - startTime) * rd.accelSpeed);

            rb.AddForce(transform.up * force);

            if (isBraking) {

                rb.AddForce(transform.up * -force);
            } else {
                rb.drag = 10;
                dragControl = 0;
            }


        }

        float rSpeed = rd.rotSpeed + UpgradeManager.Instance.GetUpgradeValue(UpgradeManager.UpgradeEnum.TURN_RADIUS) * rotUpgradeMult;
        if (Vector3.Distance(gameObject.transform.position, lastPos) > 0) {
            rb.MoveRotation(rb.rotation - moveHorizontal * rSpeed);
        } else {
            rb.MoveRotation(rb.rotation + moveHorizontal * rSpeed);
        }
        lastPos = gameObject.transform.position;

        if (isReversing) {
            currReverseTime += Time.deltaTime * 1000;
            if (currReverseTime >= reverseDuration) {
                currReverseTime = 0;
                isReversing = false;
                startTime = Time.time;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        // Trigger a reverse after collision with obstacle
        if (other.gameObject.CompareTag("Obstacle")) {
            bounceVelocity = 2;
            reverseDuration = 500;
            isReversing = true;
        } else if (other.gameObject.CompareTag("AnimalObstacle")) {
            bounceVelocity = 7;
            reverseDuration = 700;
            isReversing = true;
        }
    }
}