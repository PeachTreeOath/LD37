using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoombaController : MonoBehaviour {

    private Rigidbody2D rb;
	RoombaData rd;
	public float rotUpgradeMult;

	Vector3 lastPos;
	float startTime;

    // Use this for initialization
    void Start () {
        rb = GetComponent<Rigidbody2D>();
		rd = GetComponent<RoombaData>();
		lastPos = gameObject.transform.position;
		startTime = Time.time;
    }
    
    // Update is called once per frame
    void Update () {
        transform.FindChild("RoombaBody").rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, 0, 0);
    }

    void FixedUpdate() {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

		rb.AddForce(transform.up * - Mathf.Lerp(rd.minMoveSpeed, rd.maxMoveSpeed, (Time.time - startTime) * rd.accelSpeed));

		float rSpeed = rd.rotSpeed + UpgradeManager.Instance.GetUpgradeValue(UpgradeManager.UpgradeEnum.TURN_RADIUS) * rotUpgradeMult;
		if (Vector3.Distance(gameObject.transform.position, lastPos) > 0) {
			rb.MoveRotation(rb.rotation - moveHorizontal * rSpeed);
        } else {
			rb.MoveRotation(rb.rotation + moveHorizontal * rSpeed);
        }
		lastPos = gameObject.transform.position;
    }

    void OnTriggerEnter2D(Collider2D other) {
        /* TODO: dependent on dirt objects existing
        if (other.gameObject.CompareTag("Dirt")) {
            other.gameObject.SetActive(false);
        }
        */
    }
}
