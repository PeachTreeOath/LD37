using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class SpriteFlipper : MonoBehaviour
{

    public float flipTime;

    private float nextFlipTime;
    private SpriteRenderer sprite;

    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
		if (MyTime.Instance.time > nextFlipTime)
        {
            sprite.flipX = !sprite.flipX;
			nextFlipTime = MyTime.Instance.time + flipTime;
        }
    }
}
