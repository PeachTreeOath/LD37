using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MuteController : MonoBehaviour
{

    public bool muted;
    private SpriteRenderer slashSprite;

    void Start()
    {
        slashSprite = transform.FindChild("Slash").GetComponent<SpriteRenderer>();
        Toggle(muted);
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
