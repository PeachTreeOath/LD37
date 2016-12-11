using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradePanelShowHide : Singleton<UpgradePanelShowHide>
{
    protected override void Awake()
    {
        base.Awake();
        gameObject.SetActive(false);
    }

    /// <summary>
    /// Provides public mechanism to show/hide the upgrade panel.
    /// </summary>
    /// <param name="state"></param>
	public void ShowHide(bool state)
    {
        gameObject.SetActive(state);
        Time.timeScale = 1;
    }
}