using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxerMovementScript : MonoBehaviour
{
    Vector3 startPosition;
    bool moveTo = true;
    float timer = 0;
    // Use this for initialization
    void Start()
    {
        startPosition = this.transform.position;
        
    }

    // Update is called once per frame
    void Update()
    {
        if (moveTo)
        {
            this.transform.position += new Vector3(0.1f, 0) * Time.deltaTime;
        }
        else
        {
            if( this.transform.position != startPosition)
            {
                this.transform.position -= new Vector3(0.1f, 0) * Time.deltaTime;
            }
            else
            {
                moveTo = true;
            }
        }
       

    }
    
    void OnCollisionEnter(Collision col)
    {
        Debug.Log("bah");
    }
}
