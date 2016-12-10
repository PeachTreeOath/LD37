using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoneyTracker : MonoBehaviour {

    private Text text;

    private UpgradeManager manager;
    void Start()
    {
        text = GetComponent<Text>();
        GameObject obj = GameObject.FindGameObjectWithTag("UpManage");
        manager = obj.GetComponent<UpgradeManager>();
        
    }
	// Update is called once per frame
	void Update () {
		text.text = UpgradeManager.money.ToString();
	}
}
