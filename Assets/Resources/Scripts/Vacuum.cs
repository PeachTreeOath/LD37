using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vacuum : MonoBehaviour {

	CircleCollider2D cirCol;
	float startRad;
	public float radUpgradeMult;
	RoombaData rd;

	void Start()
	{
		rd = gameObject.transform.parent.gameObject.GetComponent<RoombaData>();
		cirCol = GetComponent<CircleCollider2D>();
		startRad = cirCol.radius;
	}

	void Update()
	{
		cirCol.radius = startRad + (UpgradeManager.Instance.GetUpgradeValue(UpgradeManager.UpgradeEnum.CLEAN_RADIUS) * radUpgradeMult);
	}
}
