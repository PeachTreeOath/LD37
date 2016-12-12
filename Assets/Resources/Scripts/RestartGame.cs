using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RestartGame : MonoBehaviour {

	public Button button;

	private void Start()
	{
		button.onClick.AddListener(delegate { ClosePanel(); });
	}

	private void ClosePanel()
	{
		UpgradeManager.Instance.Reset();
		UpgradePanelShowHide.instance.ShowHide(false);
		Transform xform = gameObject.transform;
		while(xform.parent != null)
		{
			xform = xform.parent;
		}
		Destroy(xform.gameObject);
		SceneTransitionManager.instance.ReloadRoom();
	}
}
