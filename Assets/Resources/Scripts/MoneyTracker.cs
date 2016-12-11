using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoneyTracker : MonoBehaviour
{
    private Text text;

    private void Start()
    {
        text = GetComponent<Text>();
    }

    // Update is called once per frame
    private void Update()
    {
        text.text = UpgradeManager.instance.money.ToString();
    }
}