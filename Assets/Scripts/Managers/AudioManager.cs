using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource audioSource;

    //EGG
    public AudioClip eggTap, eggCrack;
    //PATTING
    public AudioClip babyPat, adultPat, oldAgePat;

    public AudioClip clickableClicked, playingNodeAction, eatingNodeAction, workingNodeAction, trainingNodeAction;

    //LIFE CYCLE
    public AudioClip becomeAdult, becomeOld, die;
    
    //BELOW STAT ZERO
    public AudioClip hungryTummy, sadCry;

    private void Awake()
    {
        audioSource = gameObject.GetComponent<AudioSource>();
    }

    private void Start()
    {
        EventsManager.instance.CharacterEggClickedEvent += () => { audioSource.PlayOneShot(eggTap); };
        EventsManager.instance.CharacterEggHatchedEvent += (Vector3 loc) => { audioSource.PlayOneShot(eggCrack); };
        EventsManager.instance.PatThePetEvent += DoPat;

        EventsManager.instance.FoodNodeClickedEvent += OnNodeClicked;
        EventsManager.instance.WorkNodeClickedEvent += OnNodeClicked;
        EventsManager.instance.PlayNodeClickedEvent += OnNodeClicked;
        EventsManager.instance.TrainNodeClickedEvent += OnNodeClicked;

        EventsManager.instance.FoodEnergyBelowZeroEvent += FoodBelowZero;
        EventsManager.instance.HappinessBelowZeroEvent += HappinessBelowZero;

        EventsManager.instance.CharacterLifeStageChangedEvent += LifeStageChanged;

        EventsManager.instance.FoodAddedByNodeEvent += (float qty) => { NodeActionEvent(Nodes.FOOD); }; //Nodes.FOOD
        EventsManager.instance.HappinessAddedByNodeEvent += (float qty) => { NodeActionEvent(Nodes.PLAY); }; //Nodes.PLAY
        EventsManager.instance.WorkRateChangedByTrainNodeEvent += (float qty) => { NodeActionEvent(Nodes.TRAIN); }; //Nodes.TRAIN
        EventsManager.instance.ResourcesChangedByNodeEvent += (float qty) => { NodeActionEvent(Nodes.WORK); }; //Nodes.WORK
    }

    float nodeActionSoundCooldown = 2.5f;
    float nodeActionCooldownRemaining = 0;
    float belowZeroCooldownTime = 5f;
    float happinessBelowZeroCooldown = 0;
    float foodBelowZeroCooldown = 0;
    float patAudioCooldown = 3f;
    float patAudioCooldownRemaining = 0;
    private void Update()
    {
        patAudioCooldownRemaining -= Time.deltaTime;
        happinessBelowZeroCooldown -= Time.deltaTime;
        foodBelowZeroCooldown -= Time.deltaTime;
        nodeActionCooldownRemaining -= Time.deltaTime;
    }

    private void NodeActionEvent(Nodes nodeType)
    {
        if (nodeActionCooldownRemaining > 0) return;
        nodeActionCooldownRemaining = nodeActionSoundCooldown;

        switch (nodeType)
        {
            case Nodes.FOOD:
                audioSource.PlayOneShot(eatingNodeAction);
                break;
            case Nodes.WORK:
                audioSource.PlayOneShot(workingNodeAction);
                break;
            case Nodes.PLAY:
                Debug.Log("Play");
                audioSource.PlayOneShot(playingNodeAction);
                break;
            case Nodes.TRAIN:
                audioSource.PlayOneShot(trainingNodeAction);
                break;
            default:
                break;
        }
    }


    private void LifeStageChanged(CharacterLifeStage lifeStage)
    {
        switch (lifeStage)
        {
            case CharacterLifeStage.BABY:
                break;
            case CharacterLifeStage.ADULT:
                audioSource.PlayOneShot(becomeAdult);
                break;
            case CharacterLifeStage.OLD_AGE:
                audioSource.PlayOneShot(becomeOld);
                break;
            case CharacterLifeStage.DEAD:
                audioSource.PlayOneShot(die);
                break;
            default:
                break;
        }
    }


    void HappinessBelowZero()
    {
        if (happinessBelowZeroCooldown > 0) return;
        happinessBelowZeroCooldown = belowZeroCooldownTime;
        audioSource.PlayOneShot(sadCry);
    }

    void FoodBelowZero()
    {
        if (foodBelowZeroCooldown > 0) return;
        foodBelowZeroCooldown = belowZeroCooldownTime;
        audioSource.PlayOneShot(hungryTummy);
    }

    void OnNodeClicked()
    {
        //Should we check if this is the same node?
        nodeActionCooldownRemaining = 0;

        audioSource.PlayOneShot(clickableClicked);
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
