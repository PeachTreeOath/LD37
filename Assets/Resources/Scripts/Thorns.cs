using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Thorns : MonoBehaviour
{
    private float timer;
    private float timeout = .25f;

    // Use this for initialization
    private void Start()
    {
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Thorns"), (LayerMask.NameToLayer("Default")));
		timer = MyTime.Instance.time;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
		if (MyTime.Instance.time - timer > timeout &&
            col.gameObject.tag.Equals("AnimalObstacle"))
        {
			timer = MyTime.Instance.time;
            bool destr = UpgradeManager.Instance.Downgrade(UpgradeManager.UpgradeEnum.THORNS);
            //bool destr = GameManager.instance.RemoveThorns();
            Controller.RandomTravelBehavior rtb = col.gameObject.GetComponent<Controller.RandomTravelBehavior>();
            Vector3 pDir = col.gameObject.transform.position - gameObject.transform.position;
            rtb.KnockBack(pDir.normalized);
            if (destr)
            {
                Destroy(gameObject);
            }
        }
    }
}