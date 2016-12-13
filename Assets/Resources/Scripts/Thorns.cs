using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Thorns : MonoBehaviour
{
	public GameObject player;
	private float timer;
	private float timeout = .25f;

	bool queueDeath;

	// Use this for initialization
	void Start()
	{
		queueDeath = false;
		Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Thorns"), (LayerMask.NameToLayer("Default")));
		timer = MyTime.Instance.time;
	}

	void Update()
	{
		if(queueDeath)
		{
			gameObject.transform.position = player.transform.position;
			if(gameObject.GetComponent<Flash>() == null)
			{
				Destroy(gameObject);
			}
		}
	}

	void OnTriggerEnter2D(Collider2D col)
	{
		if (MyTime.Instance.time - timer > timeout &&
			col.gameObject.tag.Equals("AnimalObstacle"))
		{
			if(!queueDeath &&
				gameObject.GetComponent<Flash>() == null)
			{
				gameObject.AddComponent<Flash>().numFlashes = 3;
				//Debug.Log("Thorns trigger");
				timer = MyTime.Instance.time;
				bool destr = UpgradeManager.Instance.Downgrade(UpgradeManager.UpgradeEnum.THORNS);
				Controller.RandomTravelBehavior rtb = col.gameObject.GetComponent<Controller.RandomTravelBehavior>();
				Vector3 pDir = col.gameObject.transform.position - gameObject.transform.position;
				rtb.KnockBack(pDir.normalized);
				if (destr)
				{
					queueDeath = true;
				}
			}
		}
	}
}