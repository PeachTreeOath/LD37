using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoneyTracker : MonoBehaviour {

    private Text text;

    void Start()
    {
        text = GetComponent<Text>();
    }
	// Update is called once per frame
	void Update () {
		text.text = UpgradeManager.money.ToString();
	}
}
