using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeButton : MonoBehaviour {

    public Text ButtonText;
    public int MaxLevel;
    public Text Money;
    public Text Cost;
    public Text Description;
    
    public void Upgrade()
    {
        int value = int.Parse(ButtonText.text);
        int moneyValue = int.Parse(Money.text);
        if(value < MaxLevel)
        {
            value++;
        }
        ButtonText.text = value.ToString();
    }

}
