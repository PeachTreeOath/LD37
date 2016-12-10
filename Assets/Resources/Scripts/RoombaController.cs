using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoombaController : MonoBehaviour {

    private Rigidbody2D rb;
	RoombaData rd;

	Vector3 lastPos;
    // Use this for initialization
    void Start () {
        rb = GetComponent<Rigidbody2D>();
		rd = GetComponent<RoombaData>();
		lastPos = gameObject.transform.position;
    }
    
    // Update is called once per frame
    void Update () {
        transform.FindChild("RoombaBody").rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, 0, 0);
    }

    void FixedUpdate() {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

		rb.AddForce(transform.up * -rd.moveSpeed);

		if (Vector3.Distance(gameObject.transform.position, lastPos) > 0) {
			rb.MoveRotation(rb.rotation - moveHorizontal * rd.rotSpeed);
        } else {
			rb.MoveRotation(rb.rotation + moveHorizontal * rd.rotSpeed);
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
