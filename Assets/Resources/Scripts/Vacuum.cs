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

		UpgradeManager.Instance.AddUpgrade(UpgradeManager.UpgradeEnum.CLEAN_RADIUS, false);
	}

	void Update()
	{
		cirCol.radius = startRad + (UpgradeManager.Instance.GetUpgradeValue(UpgradeManager.UpgradeEnum.CLEAN_RADIUS) * radUpgradeMult);
	}

	void OnTrigger2DEnter(Collider2D col)
	{
		if(col.gameObject.tag.Equals("Dirt"))
		{
			DirtData dirt = col.gameObject.GetComponent<DirtData>();
			dirt.value -= (int)((rd.suctionPower + UpgradeManager.Instance.GetUpgradeValue(UpgradeManager.UpgradeEnum.DEEP_CLEAN)) * dirt.multFactor);

			Color origCol = col.gameObject.GetComponent<SpriteRenderer>().color;
			origCol.a = dirt.value/dirt.baseValue;
			col.gameObject.GetComponent<SpriteRenderer>().color = origCol;
			if(dirt.value <= 0)
			{
				Destroy(col.gameObject);
			}
		}
	}
}
