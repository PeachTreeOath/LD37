using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : Singleton<AudioManager>
{
    
    private AudioSource musicChannel;
    private AudioSource soundChannel;
    private AudioSource dirtChannel; // To play dirt repeatedly
    private Dictionary<string, AudioClip> soundMap;

    private int dirtStockpile = 0;
    private bool playingDirtSound = false;
    private float timeSinceLastSound = 0;
    private float dirtCheckInterval = .18f;
    // Use this for initialization
    protected override void Awake()
    {
        base.Awake();

        soundMap = new Dictionary<string, AudioClip>();

        musicChannel = Instantiate(Resources.Load<GameObject>("Prefabs/AudioChannel")).GetComponent<AudioSource>();
        musicChannel.transform.SetParent(transform);
        musicChannel.loop = true;
        soundChannel = Instantiate(Resources.Load<GameObject>("Prefabs/AudioChannel")).GetComponent<AudioSource>();
        soundChannel.transform.SetParent(transform);
        dirtChannel = Instantiate(Resources.Load<GameObject>("Prefabs/AudioChannel")).GetComponent<AudioSource>();
        dirtChannel.transform.SetParent(transform);
        //dirtChannel.loop = true;

        AudioClip[] clips = Resources.LoadAll<AudioClip>("Audio");
        foreach (AudioClip clip in clips)
        {
            soundMap.Add(clip.name, clip);
        }

        //PlayMusicWithIntro("suck_it_up_intro", "suck_it_up_loop", .25f);
        //roombaChannel.clip = soundMap["roomba"];
        //roombaChannel.Play();
    }

    // Pretty optional, keeping this here in case
    public void PlayShopMusic(float volume)
    {
        PlayMusic("roomba_rousey_shop", volume);
    }

    public void PlayRoomMusic(float volume)
    {
        PlayMusicWithIntro("suck_it_up_intro", "suck_it_up_loop", volume);
        //roombaChannel.clip = soundMap["roomba"];
        //roombaChannel.Play();
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

    public void PlayDirtSound(int dirtAmmount)
    {
        dirtStockpile += dirtAmmount;
        
    }

    public void PlaySound(string name, float volume)
    {
        soundChannel.PlayOneShot(soundMap[name], volume);
    }

    public void ToggleMute(bool mute)
    {
        if(mute)
        {
            AudioListener.volume = 0;
        }
        else
        {
            AudioListener.volume = 1;
        }
    }

    public void Update()
    {
        timeSinceLastSound += Time.deltaTime;

        playingDirtSound = timeSinceLastSound < dirtCheckInterval; 
        if (!playingDirtSound && dirtStockpile >= 60)
        {
            playingDirtSound = true;
            dirtStockpile -= 60;
            timeSinceLastSound = 0;
            AudioClip clip = soundMap["Crumb_click_heavy"];
            soundChannel.PlayOneShot(clip);
        }
        else if (!playingDirtSound && dirtStockpile >= 30)
        {
            playingDirtSound = true;
            dirtStockpile -= 30;
            timeSinceLastSound = 0;
            AudioClip clip = soundMap["Crumb_click_med"];
            soundChannel.PlayOneShot(clip);
        }
        else if (!playingDirtSound && dirtStockpile >= 8)
        {
            playingDirtSound = true;
            dirtStockpile -= 8;
            timeSinceLastSound = 0;
            AudioClip clip = soundMap["Crumb_click_light"];
            soundChannel.PlayOneShot(clip);
        }
    }
}
