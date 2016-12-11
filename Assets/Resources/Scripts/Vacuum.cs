using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vacuum : MonoBehaviour
{
    private CircleCollider2D cirCol;
    private float startRad;
    public float radUpgradeMult;
    private RoombaData rd;

    private void Start()
    {
        rd = gameObject.transform.parent.gameObject.GetComponent<RoombaData>();
        cirCol = GetComponent<CircleCollider2D>();
        startRad = cirCol.radius;
    }

    private void Update()
    {
        cirCol.radius = startRad + (UpgradeManager.instance.GetUpgradeLevel(UpgradeManager.UpgradeEnum.CLEAN_RADIUS) * radUpgradeMult);
    }
}