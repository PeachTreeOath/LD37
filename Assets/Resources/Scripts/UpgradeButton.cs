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
        GameObject manager = GameObject.FindGameObjectWithTag("UpManage");
        upManager = manager.GetComponent<UpgradeManager>();
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
            upManager.money -= costValue;
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
                upManager.thornLevel = value;
                break;
            case "Eagle Eye Button":
                upManager.visionLevel = value;
                break;
            case "Muscle Mix of Mixing":
                upManager.energyLevel = value;
                break;
            case "Max Gainz":
                upManager.deepCleanLevel = value;
                break;
            case "AoE Cleaning":
                upManager.cleanRadiusLevel = value;
                break;
            case "Rhonda's Legs of Running":
                upManager.speedLevel = value;
                break;
            case "Rhonda's Rockets of Launching":
                upManager.launchSpeedLevel = value;
                break;
            case "Rhonda's Omnidirectional Turning Ability":
                upManager.turningLevel = value;
                break;
        }
    }
    public void SetDescription()
    {
        DescriptionField.text = Description.text;
    }

}
