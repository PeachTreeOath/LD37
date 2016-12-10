using System.Collections;
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
        transform.FindChild("RoombaBody").rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, 0, 0);
    }

    void FixedUpdate() {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        Vector2 moveForward = new Vector2(0, MovementSpeed * moveVertical);
        rb.AddForce(transform.up * -MovementSpeed * moveVertical);

        if (moveVertical >= 0) {
            rb.MoveRotation(rb.rotation - moveHorizontal * RotationSpeed);
        } else {
            rb.MoveRotation(rb.rotation + moveHorizontal * RotationSpeed);
        }
    }

    void OnTriggerEnter2D(Collider2D other) {
        /* TODO: dependent on dirt objects existing
        if (other.gameObject.CompareTag("Dirt")) {
            other.gameObject.SetActive(false);
        }
        */
    }
}
