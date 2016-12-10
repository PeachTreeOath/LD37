using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : Singleton<AudioManager>
{

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
        SELECT,
        SUCK_IT_UP_INTRO,
        SUCK_IT_UP_LOOP,
        ROOMBA_ROUSEY_SHOP
    }

    private AudioSource musicChannel;
    private AudioSource soundChannel;
    private Dictionary<string, AudioClip> soundMap;

    // Use this for initialization
    void Start()
    {
        soundMap = new Dictionary<string, AudioClip>();

        musicChannel = Instantiate(Resources.Load<GameObject>("Prefabs/AudioChannel")).GetComponent<AudioSource>();
        musicChannel.transform.SetParent(transform);
        musicChannel.loop = true;
        soundChannel = Instantiate(Resources.Load<GameObject>("Prefabs/AudioChannel")).GetComponent<AudioSource>();
        soundChannel.transform.SetParent(transform);

        AudioClip[] clips = Resources.LoadAll<AudioClip>("Audio");
        foreach (AudioClip clip in clips)
        {
            Debug.Log(clip.name);
        }
        PlayRoomMusic(100);

        
    }
    public void PlayShopMusic(float volume)
    {
        musicChannel.PlayOneShot(soundMap["ROOMBA_ROUSEY_SHOP"], volume);
    }

    public void PlayRoomMusic(float volume)
    {
        musicChannel.PlayOneShot(soundMap["SUCK_IT_UP_LOOP"], volume);
    }


    public void PlaySound(string name)
    {
        soundChannel.PlayOneShot(soundMap[name]);
    }

    public void PlaySound(string name, float volume)
    {
        soundChannel.PlayOneShot(soundMap[name], volume);
    }
}
