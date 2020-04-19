using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIAudioManager : MonoBehaviour
{
    AudioSource audioSource;
    [SerializeField] AudioClip popupButtonPress, spawnButtonPress;

    private void Awake()
    {
        audioSource = gameObject.GetComponent<AudioSource>();
    }

    public void PopupButtonPressed()
    {
        audioSource.PlayOneShot(popupButtonPress);
    }

    public void SpawnButtonPressed()
    {
        audioSource.PlayOneShot(spawnButtonPress);
    }
}
