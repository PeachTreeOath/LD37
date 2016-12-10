using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeDisplay : MonoBehaviour {

	public List<Texture2D> images;

	GameObject imgRoot;
	GameObject txtRoot;

	List<GameObject> imgObjs;
	List<GameObject> txtObjs;

	// Use this for initialization
	void Start () {
		UpgradeManager.Instance.GetUpgradeValue(UpgradeManager.UpgradeEnum.DEEP_CLEAN);
		imgRoot = gameObject.transform.FindChild("Images").gameObject;
		txtRoot = gameObject.transform.FindChild("Texts").gameObject;

		imgObjs = new List<GameObject>();
		txtObjs = new List<GameObject>();
		for(int i = 0; i < imgRoot.transform.childCount; i++)
		{
			imgObjs.Add(imgRoot.transform.GetChild(i).gameObject);
		}
		for(int i = 0; i < txtRoot.transform.childCount; i++)
		{
			txtObjs.Add(txtRoot.transform.GetChild(i).gameObject);
		}

		imgObjs.Sort((x, y) => string.Compare(((GameObject)x).name, ((GameObject)y).name));
		txtObjs.Sort((x, y) => string.Compare(((GameObject)x).name, ((GameObject)y).name));

		for(int i = 0; i < imgObjs.Count; i++)
		{
			imgObjs[i].GetComponent<Image>().sprite = Sprite.Create(images[i], new Rect(0, 0, images[i].width, images[i].height), Vector2.zero);
		}
	}
	
	// Update is called once per frame
	void Update () {
		for(int i = 0; i < txtObjs.Count; i++)
		{
			txtObjs[i].GetComponent<Text>().text = UpgradeManager.Instance.GetUpgradeValue((UpgradeManager.UpgradeEnum)i) + "/5";
		}
	}
}
