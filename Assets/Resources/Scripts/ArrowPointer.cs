using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowPointer : MonoBehaviour
{
    public GameObject roombaBase;

    public float scale;
    public float offset;

    private GameObject arrow;
    // If you change this value, change it in AnimateFillAmount too
    private float batteryThreshold = .15f;

    private RoombaData roombaData;

    // Use this for initialization
    private void Start()
    {
        arrow = Instantiate(Resources.Load<GameObject>("Prefabs/BaseArrow"));
        arrow.transform.position = roombaBase.transform.position;
        arrow.transform.SetParent(roombaBase.transform);
        arrow.SetActive(false);
        roombaData = GameObject.Find("RoombaUnit").GetComponent<RoombaData>();
    }

    // Update is called once per frame
    private void LateUpdate()
    {
        if (roombaData != null && roombaData.curBatteryPerc <= batteryThreshold)
        {
            if (!arrow.activeInHierarchy)
            {
                arrow.SetActive(true);
            }
            if (CheckIfOnScreen(roombaBase))
            {
				float bounceVal = Mathf.Abs(Mathf.Sin(MyTime.Instance.time * 3) / 3);
                arrow.transform.position = roombaBase.transform.position;
                arrow.transform.up = roombaBase.transform.position - (arrow.transform.position + arrow.transform.localPosition);
                arrow.transform.localPosition = new Vector2(0, bounceVal + offset);
                arrow.transform.localScale = new Vector2(scale, -scale);
            }
            else
            {
                Vector3 llBounds = Camera.main.ViewportToWorldPoint(new Vector3(0.05f, 0.1f));
                Vector3 urBounds = Camera.main.ViewportToWorldPoint(new Vector3(0.95f, 0.90f));

                arrow.transform.position = new Vector2(Mathf.Clamp(roombaBase.transform.position.x, llBounds.x, urBounds.x), Mathf.Clamp(roombaBase.transform.position.y, llBounds.y, urBounds.y));
                Quaternion rotation = Quaternion.LookRotation(roombaBase.transform.position - arrow.transform.position, arrow.transform.TransformDirection(Vector3.back));
                arrow.transform.rotation = new Quaternion(0, 0, rotation.z, rotation.w);
                arrow.transform.localScale = new Vector2(scale, scale);
            }
        }
    }

    private bool CheckIfOnScreen(GameObject obj)
    {
        Vector2 point = Camera.main.WorldToViewportPoint(obj.transform.position);

        if (point.x > 0 && point.y > 0 && point.x < 1 && point.y < 1)
        {
            return true;
        }
        return false;
    }
}