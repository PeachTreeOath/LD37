using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class SceneTransitionManager : Singleton<SceneTransitionManager>
{

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void GoToShop ()
	{
		SceneManager.LoadScene ("UpgradeScreen");
	}

	public void GoToRoom ()
	{
		SceneManager.LoadScene ("Game");
	}
}
