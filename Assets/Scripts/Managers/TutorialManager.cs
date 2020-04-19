using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    //Just going to handle tutorial audio here because... well just because.
    AudioSource audioSource;
    public AudioClip popupAudio;

    private void Start()
    {
        audioSource = gameObject.GetComponent<AudioSource>();
        StartTutorial();
    }

    public GameObject allTutorialUI;
    //Intro
    public GameObject tutorialIntroPopup;
    public void StartTutorial()
    {
        allTutorialUI.SetActive(true);
        tutorialIntroPopup.SetActive(true);
        audioSource.PlayOneShot(popupAudio);
        EventsManager.instance.CharacterEggHatchedEvent += EggHatched;
    }


    public CharacterStats timtamPrefab;
    CharacterStats timtam;
    private void EggHatched(Vector3 hatchLocation)
    {
        EventsManager.instance.CharacterEggHatchedEvent -= EggHatched;

        tutorialIntroPopup.SetActive(false);
        timtam = Instantiate(timtamPrefab, hatchLocation, Quaternion.identity);
        timtam.degradeWithTime = false;
        timtam.SetInitStats(70, 70);
        EventsManager.instance.FirePlayerSpawnedEvent(timtam);

        StartCoroutine(DoPat());
    }

    public GameObject tutorialPatPopup;
    IEnumerator DoPat()
    {
        yield return new WaitForSeconds(0.5f);
        EventsManager.instance.PatThePetEvent += PatDone;
        tutorialPatPopup.SetActive(true);
        audioSource.PlayOneShot(popupAudio);
    }

    bool patRemoved = false;
    void PatDone()
    {
        EventsManager.instance.PatThePetEvent -= PatDone;
        patRemoved = true;
        tutorialPatPopup.SetActive(false);
        StartCoroutine(DoPlay());
    }


    public PlayNode playNode;
    PlayNode spawnedPlayNode;
    public GameObject tutorialPlayPopup;
    public Transform playLoc;
    IEnumerator DoPlay()
    {
        yield return new WaitForSeconds(1.5f);
        spawnedPlayNode = Instantiate(playNode, playLoc.position, Quaternion.identity);
        EventsManager.instance.PlayNodeClickedEvent += PlayDone;
        tutorialPlayPopup.SetActive(true);
        audioSource.PlayOneShot(popupAudio);
    }

    void PlayDone()
    {
        EventsManager.instance.PlayNodeClickedEvent -= PlayDone;
        tutorialPlayPopup.SetActive(false);
        StartCoroutine(DoFood());
    }


    public FoodNode foodNode;
    FoodNode spawnedFoodNode;
    public GameObject tutorialFoodPopup;
    public Transform foodLoc;
    IEnumerator DoFood()
    {
        yield return new WaitForSeconds(3);
        if (spawnedPlayNode != null) Destroy(spawnedPlayNode.gameObject);

        spawnedFoodNode = Instantiate(foodNode, foodLoc.position, Quaternion.identity);
        EventsManager.instance.FoodNodeClickedEvent += FoodDone;
        tutorialFoodPopup.SetActive(true);
        audioSource.PlayOneShot(popupAudio);
    }

    void FoodDone()
    {
        EventsManager.instance.FoodNodeClickedEvent -= FoodDone;
        tutorialFoodPopup.SetActive(false);
        StartCoroutine(DoWork());
    }



    public ResourceNode workNode;
    ResourceNode spawnedWorkNode;
    public GameObject tutorialWorkPopup;
    public Transform workLoc;
    IEnumerator DoWork()
    {
        yield return new WaitForSeconds(3);
        if (spawnedFoodNode != null) Destroy(spawnedFoodNode.gameObject);

        spawnedWorkNode = Instantiate(workNode, workLoc.position, Quaternion.identity);
        EventsManager.instance.WorkNodeClickedEvent += WorkDone;
        tutorialWorkPopup.SetActive(true);
        audioSource.PlayOneShot(popupAudio);
    }
    

    void WorkDone()
    {
        EventsManager.instance.WorkNodeClickedEvent -= WorkDone;
        tutorialWorkPopup.SetActive(false);
        StartCoroutine(FinishTutorial());
    }


    public GameObject tutorialFinishedPopup;
    IEnumerator FinishTutorial()
    {
        yield return new WaitForSeconds(5);
        if (spawnedWorkNode != null) Destroy(spawnedWorkNode.gameObject);
        tutorialFinishedPopup.SetActive(true);
        audioSource.PlayOneShot(popupAudio);
    }

    public GameObject UItoEnable;
    public Transform liveWorkLoc;
    public void EndTutorial()
    {
        tutorialFinishedPopup.SetActive(false);
        if (patRemoved == false)
            EventsManager.instance.PatThePetEvent -= PatDone;

        allTutorialUI.SetActive(false);


        timtam.degradeWithTime = true; //This should already be done but doing it here again to be safe
        EventsManager.instance.FireEndTutorialEvent();

        //SET INITIAL CONDITIONS
        //UI update etc
        //All can probably be done by responding to the event??
        timtam.SetInitStats(100, 100);
        Instantiate(workNode, liveWorkLoc.position, Quaternion.identity);

        UItoEnable.SetActive(true);
    }

}
