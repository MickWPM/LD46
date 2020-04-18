using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterGraphicsManager : MonoBehaviour
{
    [SerializeField] GameObject babyGraphics, adultGraphics, matureAdultGraphics, oldAgeGraphics;
    [SerializeField] Sprite mouth, happyMouth, sadMouth;
    [SerializeField] SpriteRenderer mouthRenderer;

    [SerializeField] Sprite normalBelly, hungryBelly, fullBelly;
    [SerializeField] SpriteRenderer bellyRenderer;


    // Start is called before the first frame update
    void Start()
    {
        HideLifeGraphics();

        babyGraphics.SetActive(true);

        mouthRenderer.sprite = mouth;
        bellyRenderer.sprite = normalBelly;

        EventsManager.instance.ChangeHappinessStateEvent += OnChangeHappiness;
        EventsManager.instance.ChangeHungerStateEvent += OnChangeHunger;
        EventsManager.instance.CharacterLifeStageChangedEvent += OnLifeStageChanged;

    }

    private void OnLifeStageChanged(CharacterLifeStage lifeStage)
    {
        HideLifeGraphics();
        switch (lifeStage)
        {
            case CharacterLifeStage.BABY:
                babyGraphics.SetActive(true);
                break;
            case CharacterLifeStage.ADULT:
                adultGraphics.SetActive(true);
                break;
            case CharacterLifeStage.OLD_AGE:
                oldAgeGraphics.SetActive(true);
                break;
            case CharacterLifeStage.DEAD:
                Debug.Log("NO DEATH GRAPHICS");
                break;
            default:
                break;
        }
    }

    void HideLifeGraphics()
    {
        babyGraphics.SetActive(false);
        adultGraphics.SetActive(false);
        matureAdultGraphics.SetActive(false);
        oldAgeGraphics.SetActive(false);

    }

    private void OnChangeHunger(CharacterHungerState oldHunger, CharacterHungerState newHunger)
    {
        switch (newHunger)
        {
            case CharacterHungerState.FULL:
                bellyRenderer.sprite = fullBelly;
                break;
            case CharacterHungerState.NORMAL:
                bellyRenderer.sprite = normalBelly;
                break;
            case CharacterHungerState.STARVING:
                bellyRenderer.sprite = hungryBelly;
                break;
            default:
                Debug.LogError($"{newHunger} hunger state not handled");
                break;
        }
    }

    private void OnChangeHappiness(CharacterHappinessState oldHappiness, CharacterHappinessState newHappiness)
    {
        switch (newHappiness)
        {
            case CharacterHappinessState.HAPPY:
                mouthRenderer.sprite = happyMouth;
                break;
            case CharacterHappinessState.NORMAL:
                mouthRenderer.sprite = mouth;
                break;
            case CharacterHappinessState.SAD:
                mouthRenderer.sprite = sadMouth;
                break;
            default:
                break;
        }
    }


}
