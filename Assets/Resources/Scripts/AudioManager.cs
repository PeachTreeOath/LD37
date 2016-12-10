using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : Singleton<AudioManager>
{

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
            soundMap.Add(clip.name, clip);
        }
        // PlayRoomMusic(1);
        PlaySound("suck_it_up_loop", .25f);
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
