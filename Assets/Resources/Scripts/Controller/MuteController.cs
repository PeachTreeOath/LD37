using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MuteController : MonoBehaviour
{

    public bool muted;
    private float scale = 0.5f;

    private SpriteRenderer slashSprite;
    private Camera mainCam;

    void Start()
    {
        slashSprite = transform.FindChild("Slash").GetComponent<SpriteRenderer>();
        mainCam = Camera.main;
        Toggle(muted); 
    }

    void LateUpdate()
    {
        RecalculatePosition();
    }

    void RecalculatePosition()
    {
        transform.position = mainCam.ViewportToWorldPoint(new Vector3(0.95f, 0.95f));
        transform.position = (Vector2)transform.position;
        float newScale = mainCam.orthographicSize * scale;
        transform.localScale = new Vector3(newScale, newScale, 1);
    }

    void OnMouseDown()
    {
        muted = !muted;
        Toggle(muted);
    }

    private void Toggle(bool toggle)
    {
        AudioManager.instance.ToggleMute(toggle);
        if (toggle)
        {
            slashSprite.enabled = true;
        }
        else
        {
            slashSprite.enabled = false;
        }
    }
}
