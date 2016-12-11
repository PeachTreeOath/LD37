using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CurrencyDisplay : MonoBehaviour
{
    public Text textbox;

    // Use this for initialization
    private void Start()
    {
        if (textbox == null)
            textbox = GetComponent<Text>();
    }

    // Update is called once per frame
    private void Update()
    {
        textbox.text = UpgradeManager.money.ToString();
    }
}