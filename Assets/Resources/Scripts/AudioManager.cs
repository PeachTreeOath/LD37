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

        PlayMusicWithIntro("suck_it_up_intro", "suck_it_up_loop", .25f);
        roombaChannel.clip = soundMap["roomba"];
        roombaChannel.Play();
    }

    // Pretty optional, keeping this here in case
    public void PlayShopMusic(float volume)
    {
        PlayMusic("roomba_rousey_shop", volume);
    }

    public void PlayMusic(string name, float volume)
    {
        musicChannel.clip = soundMap[name];
        musicChannel.volume = volume;
        musicChannel.Play();
    }

    public void PlayMusicWithIntro(string introName, string loopName, float volume)
    {
        PlayMusic(introName, volume);
        StartCoroutine(PlayMusicDelayed(loopName, volume, musicChannel.clip.length));
    }

    IEnumerator PlayMusicDelayed(string name, float volume, float delayTime)
    {
        yield return new WaitForSeconds(delayTime);
        PlayMusic(name, volume);
    }

    public void PlaySound(string name)
    {
        AudioClip clip = soundMap[name];
        soundChannel.PlayOneShot(soundMap[name]);
    }

    public void PlaySound(string name, float volume)
    {
        soundChannel.PlayOneShot(soundMap[name], volume);
    }
}
