using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MusicManager : MonoBehaviour
{

    public AudioClip introSong, mainRepeatLoop;
    AudioSource audioSource;

    float baseVolume;
    bool on = true;
    void Awake()
    {
        audioSource = gameObject.GetComponent<AudioSource>();
        DontDestroyOnLoad(this.gameObject);
        baseVolume = audioSource.volume;
    }

    private void Start()
    {
        StartCoroutine(PlayMusic());
    }

    IEnumerator PlayMusic()
    { 
        audioSource.clip = introSong;
        audioSource.Play();
        yield return new WaitForSeconds(audioSource.clip.length);
        audioSource.clip = mainRepeatLoop;
        audioSource.Play();
    }

    public bool IsMusicOn()
    {
        return on;
    }

    public bool ToggleMusicNowOn()
    {
        on = !on;
        audioSource.volume = on ? baseVolume : 0;
        return on;
    }
}
