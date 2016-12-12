using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCameraController : MonoBehaviour
{
    public GameObject player;
    private Vector3 offset;
    private float startSize;
    private Camera cam;
    private List<ISizeListener> listeners;
    // Use this for initialization
    void Awake()
    {

        listeners = new List<ISizeListener>();
    }

    private void Start()
    {
        // Get distance from roomba starting position
        offset = transform.position - player.transform.position;
        cam = GetComponent<Camera>();
        startSize = cam.orthographicSize;
        cam.orthographicSize = startSize + UpgradeManager.Instance.GetUpgradeValue(UpgradeManager.UpgradeEnum.VISION) * .25f;
    }

    // Update is called once per frame
    private void LateUpdate()
    {
        transform.position = player.transform.position + offset;

    }

    public void VisionChange()
    {
        cam.orthographicSize = startSize + UpgradeManager.Instance.GetUpgradeValue(UpgradeManager.UpgradeEnum.VISION) * .25f;
        foreach (ISizeListener listener in listeners)
        {
            listener.SizeChanged(cam.orthographicSize);
        }
    }


    public void RegisterSizeListener(ISizeListener listener)
    {
        listeners.Add(listener);
    }


    public void DeregisterSizeListener(ISizeListener listener)
    {
        listeners.Remove(listener);
    }
}