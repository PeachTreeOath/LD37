using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnimateFillAmount : MonoBehaviour {
    public float timeLeft = 1;
    public Image image;
    public bool coolingDown;
    public float waitTime = 30.0f;

    // Use this for initialization
    void Start () {
        image = GetComponent<Image>();
    }
	
	// Update is called once per frame
	void Update () {
        //amount -= 0.1f;
        image.fillAmount -= 1.0f / waitTime * Time.deltaTime;
        if (coolingDown == true)
        {
            //Reduce fill amount over 30 seconds
            image.fillAmount -= 1.0f / waitTime * Time.deltaTime;
        }
    }
}