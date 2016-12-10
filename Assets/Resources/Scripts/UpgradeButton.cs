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
    private Text DescriptionField;
    private UpgradeManager upManager;
    public AudioClip succesClip;
    public AudioClip failureClip;
    private AudioSource audioSource;

    public void Start()
    {
		upManager = UpgradeManager.Instance;
        GameObject audioManager = GameObject.Find("AudioManager");
        audioSource = audioManager.GetComponent<AudioSource>();
        GameObject descipt = GameObject.FindGameObjectWithTag("DescriptionTag");
        DescriptionField = descipt.GetComponent<Text>();

    }
    public void Upgrade()
    {
        Button button = this.gameObject.GetComponent<Button>();
        int value = int.Parse(ButtonText.text);
        int moneyValue = int.Parse(Money.text);
        int costValue = int.Parse(Cost.text);
        if(value < MaxLevel && moneyValue >= costValue)
        {
            value++;
			UpgradeManager.money -= costValue;
            Money.text = upManager.ToString();
            UpdateManager(value);
            //Debug.Log("Hit");
            audioSource.PlayOneShot(succesClip);
        }
        else
        {
            audioSource.PlayOneShot(failureClip);
        }
        if(value == MaxLevel)
        {
            button.enabled = false;
            Text text = this.GetComponentInChildren<Text>();
            if(text != null)
            {
                text.text = "MAXED";
            }
            
        }
        ButtonText.text = value.ToString();
        
    }

    public void UpdateManager(int value)
    {
        string name = this.gameObject.name;
        switch(name)
        {
            case "Knife Button":
				upManager.AddUpgrade(UpgradeManager.UpgradeEnum.THORNS, false);
                break;
            case "Eagle Eye Button":
				upManager.AddUpgrade(UpgradeManager.UpgradeEnum.VISION, false);
                break;
            case "Muscle Mix of Mixing":
				upManager.AddUpgrade(UpgradeManager.UpgradeEnum.ENERGY, false);
                break;
            case "Max Gainz":
				upManager.AddUpgrade(UpgradeManager.UpgradeEnum.DEEP_CLEAN, false);
                break;
            case "AoE Cleaning":
				upManager.AddUpgrade(UpgradeManager.UpgradeEnum.CLEAN_RADIUS, false);
                break;
            /*case "Rhonda's Legs of Running":
                upManager.speedLevel = value;
                break;
            case "Rhonda's Rockets of Launching":
                upManager.launchSpeedLevel = value;
                break;*/
            case "Rhonda's Omnidirectional Turning Ability":
				upManager.AddUpgrade(UpgradeManager.UpgradeEnum.TURN_RADIUS, false);
                break;
        }
    }
    public void SetDescription()
    {
        DescriptionField.text = Description.text;
    }

}
