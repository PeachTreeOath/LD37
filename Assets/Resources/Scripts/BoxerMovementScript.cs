using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxerMovementScript : MonoBehaviour
{
    Vector3 startPosition;
    public float speed;
    public float reengageTime;
    private GameObject kangaroo; // Only one pairing allowed in scene
    bool moveTo = true;
    float timer = 0;

    // Use this for initialization
    void Start()
    {
        startPosition = this.transform.position;
        kangaroo = GameObject.Find("kangaroo");
    }

    // Update is called once per frame
    void Update()
    {
        if (moveTo)
        {
            timer += Time.deltaTime;
            Vector3 diff = kangaroo.transform.position - transform.position;
            transform.position += diff.normalized * speed * Time.deltaTime;
        }
        else
        {
            timer -= Time.deltaTime;
            Vector3 diff = kangaroo.transform.position - transform.position;
            transform.position -= diff.normalized * speed * Time.deltaTime;
            if (timer < 0)
            {
                moveTo = true;
            }

        }


    }
    
    void OnCollisionEnter2D(Collision2D col)
    {
        moveTo = false;
    }
}
