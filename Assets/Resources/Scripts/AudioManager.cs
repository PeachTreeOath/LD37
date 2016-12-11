using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : Singleton<AudioManager>
{

    private AudioSource musicChannel;
    private AudioSource soundChannel;
    private AudioSource roombaChannel; // To play constant vacuum noise..
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
        roombaChannel = Instantiate(Resources.Load<GameObject>("Prefabs/AudioChannel")).GetComponent<AudioSource>();
        roombaChannel.transform.SetParent(transform);
        roombaChannel.loop = true;

        AudioClip[] clips = Resources.LoadAll<AudioClip>("Audio");
        foreach (AudioClip clip in clips)
        {
            soundMap.Add(clip.name, clip);
        }
        
        PlayMusic("suck_it_up_loop", .25f);
        roombaChannel.clip = soundMap["roomba"];
        roombaChannel.Play();
    }

    public void PlayShopMusic(float volume)
    {
        musicChannel.PlayOneShot(soundMap["ROOMBA_ROUSEY_SHOP"], volume);
    }

    public void PlayMusic(string name, float volume)
    {
        musicChannel.clip = soundMap[name];
        musicChannel.volume = volume;
        musicChannel.Play();
    }

    public void PlayMusicWithIntro(string name, float volume)
    {
        musicChannel.clip = soundMap[name];
        musicChannel.volume = volume;
        musicChannel.Play();
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
