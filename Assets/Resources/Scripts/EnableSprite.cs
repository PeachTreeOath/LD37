using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableSprite : MonoBehaviour {

	// Use this for initialization
	void Start () {
		gameObject.GetComponent<SpriteRenderer>().enabled = true;
	}
}
