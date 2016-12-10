using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : Singleton<AudioManager> {

    public enum SoundClip
    {
        BOUNCE,
        BUG,
        BUZZ,
        COLLISION_SOFA,
        COLLISION_WOOD,
        CONSTANT,
        CRUMB,
        DOG,
        GENERIC_SHOOT_HIGH,
        GENERIC_SHOOT_MID,
        KITTY,
        LOW_BATTERY,
        MONEY_BUY,
        MONEY_INVALID,
        PICKUP_SOUND,
        PRESSING_START,
        ROCKETS,
        ROOMBA,
        ROOMBA_OVER_CARPET,
        SELECT
    }

    private AudioSource musicChannel;
    private AudioSource soundChannel;
    private AudioClip[] clips;

    // Use this for initialization
    void Start () {
        musicChannel = Instantiate(Resources.Load<GameObject>("Prefabs/AudioChannel")).GetComponent<AudioSource>();
        musicChannel.transform.SetParent(transform);
        musicChannel.loop = true;
        soundChannel = Instantiate(Resources.Load<GameObject>("Prefabs/AudioChannel")).GetComponent<AudioSource>();
        soundChannel.transform.SetParent(transform);

        clips = Resources.LoadAll<AudioClip>("Audio");
    }
	
    public void PlaySound(string name)
    {

    }

}
