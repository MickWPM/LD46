using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource audioSource;

    public AudioClip eggTap, eggCrack;
    public AudioClip babyPat, adultPat, oldAgePat;

    private void Awake()
    {
        audioSource = gameObject.GetComponent<AudioSource>();
    }

    private void Start()
    {
        EventsManager.instance.CharacterEggClickedEvent += () => { audioSource.PlayOneShot(eggTap); };
        EventsManager.instance.CharacterEggHatchedEvent += (Vector3 loc) => { audioSource.PlayOneShot(eggCrack); };
        EventsManager.instance.PatThePetEvent += DoPat;
    }

    float patAudioCooldown = 3f;
    float patAudioCooldownRemaining = 0;
    private void Update()
    {
        patAudioCooldownRemaining -= Time.deltaTime;
    }

    void DoPat()
    {
        if (patAudioCooldownRemaining > 0)
            return;

        patAudioCooldownRemaining = patAudioCooldown;
        switch (GameManager.instance.CurrentLifeStage())
        {
            case CharacterLifeStage.BABY:
                audioSource.PlayOneShot(babyPat);
                break;
            case CharacterLifeStage.ADULT:
                audioSource.PlayOneShot(adultPat);
                break;
            case CharacterLifeStage.OLD_AGE:
                audioSource.PlayOneShot(oldAgePat);
                break;
            case CharacterLifeStage.DEAD:
                break;
            default:
                break;
        }
    }

}
