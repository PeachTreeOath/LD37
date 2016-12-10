using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowPointer : MonoBehaviour
{

    public GameObject roombaBase;

    private GameObject arrow;

    // Use this for initialization
    void Start()
    {
        arrow = Instantiate(Resources.Load<GameObject>("Prefabs/BaseArrow"));
        arrow.transform.position = roombaBase.transform.position;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
