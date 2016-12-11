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
        audioSource = audioManager.GetComponentInChildren<AudioSource>();
        GameObject descipt = GameObject.FindGameObjectWithTag("DescriptionTag");
        DescriptionField = descipt.GetComponent<Text>();

		UpdateText();
    }

	void UpdateText()
	{
		string name = this.gameObject.name;
		Text txt = transform.GetChild(0).gameObject.GetComponent<Text>();
		int val = 0;
		UpgradeManager.UpgradeEnum enm;
		switch(name)
		{
		case "Knife Button":
			val = UpgradeManager.Instance.GetUpgradeValue(UpgradeManager.UpgradeEnum.THORNS);
			break;
		case "Eagle Eye Button":
			val = UpgradeManager.Instance.GetUpgradeValue(UpgradeManager.UpgradeEnum.VISION);
			break;
		case "Muscle Mix of Mixing":
			val = UpgradeManager.Instance.GetUpgradeValue(UpgradeManager.UpgradeEnum.ENERGY);
			break;
		case "Max Gainz":
			val = UpgradeManager.Instance.GetUpgradeValue(UpgradeManager.UpgradeEnum.DEEP_CLEAN);
			break;
		case "AoE Cleaning":
			val = UpgradeManager.Instance.GetUpgradeValue(UpgradeManager.UpgradeEnum.CLEAN_RADIUS);
			break;
		case "Rhonda's Legs of Running":
			val = UpgradeManager.Instance.GetUpgradeValue(UpgradeManager.UpgradeEnum.SPEED);
			break;
		case "Rhonda's Plate Armor":
			val = UpgradeManager.Instance.GetUpgradeValue(UpgradeManager.UpgradeEnum.DURABILITY);
			break;
		case "Rhonda's Omnidirectional Turning Ability":
			val = UpgradeManager.Instance.GetUpgradeValue(UpgradeManager.UpgradeEnum.TURN_RADIUS);
			break;
		}

		if(val == 5)
		{
			txt.text = "MAXED";
			Button button = this.gameObject.GetComponent<Button>();
			ButtonText.text = "5";
		}
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
				upManager.AddUpgrade(UpgradeManager.UpgradeEnum.THORNS, true);
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
            case "Rhonda's Legs of Running":
                upManager.AddUpgrade(UpgradeManager.UpgradeEnum.SPEED, false);
                break;
            case "Rhonda's Plate Armor":
                upManager.AddUpgrade(UpgradeManager.UpgradeEnum.DURABILITY, false);
                break;
            case "Rhonda's Omnidirectional Turning Ability":
				upManager.AddUpgrade(UpgradeManager.UpgradeEnum.TURN_RADIUS, false);
                break;
        }
    }
    public void SetDescription()
    {
        DescriptionField.text = Description.text;
    }

	public void Done()
	{
		Application.LoadLevel(0);
	}
}
