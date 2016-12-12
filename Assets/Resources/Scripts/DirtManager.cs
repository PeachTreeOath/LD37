using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DirtManager : Singleton<DirtManager>
{
	public float winPercentage;
	[HideInInspector]
    public float totalDirtValue;
    private float currDirtValue;

    private Text completionText;

    // Use this for initialization
    void Start()
    {
        completionText = GameObject.Find("DirtCompletionValue").GetComponent<Text>();
        currDirtValue = totalDirtValue;
    }

    // Done here because of crazy execution order
    public void IncreaseDirtValue(float value)
    {
        totalDirtValue += value;
        currDirtValue = totalDirtValue;
    }

    public void CalculateDamage(float dmg)
    {
        currDirtValue -= dmg;
		int compPerc = (int)((1 - currDirtValue / totalDirtValue) * 100);
		completionText.text = compPerc + "%";

		if(compPerc >= winPercentage)
		{
			if(MyTime.Instance.timeScale != 0)
			{
				MyTime.Instance.timeScale = 0;
				gameObject.AddComponent<WinTransition>();
			}
		}
    }
}
