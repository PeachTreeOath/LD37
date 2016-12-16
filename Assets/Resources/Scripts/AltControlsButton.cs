using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AltControlsButton : MonoBehaviour {

	public static bool isOn;

	public void Toggle()
	{
		isOn = GetComponent<Toggle>().isOn;
	}
}
