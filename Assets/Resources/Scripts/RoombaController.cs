using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoombaController : MonoBehaviour
{
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
    private float timeSpan = 0;

    /// <summary>
    /// Duration of time in miliseconds to reverse after a collision.
    /// </summary>
    private const double REVERSE_DURATION = 700;

    // Use this for initialization
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rd = GetComponent<RoombaData>();
        lastPos = gameObject.transform.position;
        startTime = Time.time;
    }

    // Update is called once per frame
    private void Update()
    {
        transform.FindChild("RoombaBody").rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, 0, 0);
    }

    private void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        if (isReversing)
            rb.AddForce(transform.up * Mathf.Lerp(rd.minMoveSpeed, rd.maxMoveSpeed, (Time.time - startTime) * rd.accelSpeed));
        else
            rb.AddForce(transform.up * -Mathf.Lerp(rd.minMoveSpeed, rd.maxMoveSpeed, (Time.time - startTime) * rd.accelSpeed));

        float rSpeed = rd.rotSpeed + UpgradeManager.Instance.GetUpgradeValue(UpgradeManager.UpgradeEnum.TURN_RADIUS) * rotUpgradeMult;
        if (Vector3.Distance(gameObject.transform.position, lastPos) > 0)
        {
            rb.MoveRotation(rb.rotation - moveHorizontal * rSpeed);
        }
        else
        {
            rb.MoveRotation(rb.rotation + moveHorizontal * rSpeed);
        }
        lastPos = gameObject.transform.position;

        if (isReversing)
        {
            timeSpan += Time.deltaTime * 1000;
            if (timeSpan >= REVERSE_DURATION)
            {
                timeSpan = 0;
                isReversing = false;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        /* TODO: dependent on dirt objects existing
        if (other.gameObject.CompareTag("Dirt")) {
            other.gameObject.SetActive(false);
        }
        */

        // Trigger a reverse after collision with obstacle
        if (other.gameObject.CompareTag("Obstacle"))
        {
            isReversing = true;
        }
    }
}