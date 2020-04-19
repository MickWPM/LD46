using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CharacterStatType
{
    RESOURCES,
    HUNGER,
    HAPPINESS
}

public enum CharacterLifeStage
{
    BABY,
    ADULT,
    OLD_AGE,
    DEAD
}


public enum CharacterHungerState
{
    FULL,
    NORMAL,
    STARVING
}

public enum CharacterHappinessState
{
    HAPPY,
    NORMAL,
    SAD
}


public class CharacterStats : MonoBehaviour
{
    [SerializeField] CharacterLifeStage currentLifeStage = CharacterLifeStage.BABY;
    [SerializeField] CharacterHappinessState currentHappinessState = CharacterHappinessState.NORMAL;
    [SerializeField] CharacterHungerState currentHungerState = CharacterHungerState.NORMAL;
    public bool degradeWithTime = false;
    //Do all of these change with age?
    [SerializeField] float lifespan = 300;
    float maxLifespan;
    [SerializeField] float foodEnergy = 100;
    [SerializeField] float hungerRate = 100f/30f;
    [SerializeField] float happiness = 100;
    [SerializeField] float sadnessRate = 100f/120f;
    [SerializeField] float workRate = 1.0f;
    [SerializeField] float lifespanLossRate = 1.0f;

    float speedScaleBaby = 0.3f, speedScaleAdult = 1.0f, speedScaleAged = 0.6f;
    float movementSpeedScaler = 0.3f;
    public float MovementSpeedScaler { get => movementSpeedScaler; private set => movementSpeedScaler = value; }

    internal void SetInitStats(float happiness, float food, float resources = 0)
    {
        this.happiness = happiness;
        this.foodEnergy = food;
        this.resources = resources;
    }

    internal void CheatResources()
    {
        resources = 0;
        AddResources(999);
    }

    [SerializeField] float resources = 0;
    public float Resources { get => resources; }

    float currentAge = 0;
    float timeSincePatThePet = 0;

    private void Start()
    {
        currentLifeStage = CharacterLifeStage.BABY;
        currentHappinessState = CharacterHappinessState.NORMAL;
        currentHungerState = CharacterHungerState.NORMAL;

        movementSpeedScaler = speedScaleBaby;
        maxLifespan = lifespan;
        EventsManager.instance.FoodAddedByNodeEvent += FoodNode;
        EventsManager.instance.HappinessAddedByNodeEvent += HappinessNode;
        EventsManager.instance.ResourcesChangedByNodeEvent += ResourcesNode;
        EventsManager.instance.WorkRateChangedByTrainNodeEvent += TrainNode;
        EventsManager.instance.PatThePetEvent += PatThePet;

        EventsManager.instance.NodeSpawnedEvent += PayForNode;
    }

    void PayForNode(Nodes node)
    {
        AddResources( -1 * GameManager.instance.NodeCost(node) );
    }

    float foodRemovedPerWorkSecond = 4, happinessRemovedPerWorkSecond = 2;
    void ResourcesNode(float amt)
    {
        AddResources(amt);
        //Remove happiness and food for this tick
        AddFood(Time.deltaTime * -foodRemovedPerWorkSecond);
        AddHappiness(Time.deltaTime * -happinessRemovedPerWorkSecond);
    }


    void FoodNode(float amt)
    {
        AddFood(amt);
    }

    void HappinessNode(float amt)
    {
        AddHappiness(amt);
    }

    float foodRemovedPerTrainSecond = 4, happinessRemovedPerTrainSecond = 6;
    void TrainNode(float amt)
    {
        AddWorkRate(amt);
        //Remove happiness and food for this tick
        AddFood(Time.deltaTime * -foodRemovedPerTrainSecond);
        AddHappiness(Time.deltaTime * -happinessRemovedPerTrainSecond);
    }


    void UpdateStates()
    {
        CharacterHungerState newHungerState = CharacterHungerState.NORMAL;
        CharacterHappinessState newHappinessState = CharacterHappinessState.NORMAL;

        if (happiness > 90f)
        {
            newHappinessState = CharacterHappinessState.HAPPY;
        }
        else if (happiness < 10f)
        {
            newHappinessState = CharacterHappinessState.SAD;
        }

        if (newHappinessState != currentHappinessState)
        {
            EventsManager.instance.FireChangeHappinessStateEvent(currentHappinessState, newHappinessState);
            currentHappinessState = newHappinessState;
        }


        if (foodEnergy > 90f)
        {
            newHungerState = CharacterHungerState.FULL;
        }
        else if (foodEnergy < 10f)
        {
            newHungerState = CharacterHungerState.STARVING;
        }

        if (newHungerState != currentHungerState)
        {
            EventsManager.instance.FireChangeHungerStateEvent(currentHungerState, newHungerState);
            currentHungerState = newHungerState;
        }
    }

    private void Update()
    {
        if (degradeWithTime)
        {
            timeSincePatThePet += Time.deltaTime;
            UpdateStats();
            UpdateLifespan();
        }

        UpdateStates();

        string s = "DEBUG INFO - STATS:\n";
        s += "\nCurrent age: " + currentAge;
        s += "\nCurrent life stage: " + currentLifeStage;
        s += "\nCurrent Food satisfaction: " + foodEnergy;
        s += "\nCurrent Happiness: " + happiness;
        s += "\nCurrent resources: " + Resources;

        s += "\n\nCurrent hunger rate: " + hungerRate;
        s += "\n\nCurrent sadness rate: " + sadnessRate;
        s += "\n\nCurrent work rate: " + workRate;
        s += "\n\nCurrent lifespan loss rate: " + lifespanLossRate;


        GameManager.instance.debugStatText.text = s;
    }

    float pattingRecharge = 5;
    public void PatThePet()
    {
        float pattingEffectiveness = 1.0f;
        if (timeSincePatThePet < pattingRecharge)
        {
            pattingEffectiveness *= 0.5f * timeSincePatThePet / pattingRecharge;
        }
        Debug.Log("Patting effectiveness = " + pattingEffectiveness);

        switch (currentLifeStage)
        {
            case CharacterLifeStage.BABY:
                EventsManager.instance.FireAddHappinessByNodeEvent(8 * pattingEffectiveness);
                break;
            case CharacterLifeStage.ADULT:
                EventsManager.instance.FireAddHappinessByNodeEvent(-3 * pattingEffectiveness);
                break;
            case CharacterLifeStage.OLD_AGE:
                EventsManager.instance.FireAddHappinessByNodeEvent(2 * pattingEffectiveness);
                break;
            case CharacterLifeStage.DEAD:
                break;
            default:
                break;
        }
        timeSincePatThePet = 0;
    }

    float babyRelativeAge = 0.2f;
    float OldAgeRelativeAge = 0.65f;

    public CharacterLifeStage GetCurrentLifeStage()
    {
        return currentLifeStage;
    }

    public float GetHappinessLifespanLossModifier()
    {
        switch (currentHappinessState)
        {
            case CharacterHappinessState.HAPPY:
                return 0.9f;
            case CharacterHappinessState.NORMAL:
                return 1;
            case CharacterHappinessState.SAD:
                return 1.5f;
            default:
                Debug.LogError($"Happiness state {currentHappinessState} not handled");
                break;
        }
        return 1;
    }

    public float GetHungerLifespanLossModifier()
    {
        if (foodEnergy > 0.9f)
            return 0.9f;

        if (foodEnergy > 0.5f)
            return 1;

        if (foodEnergy > 0.25f)
            return 1.25f;

        return 1.5f;
    }

    public float GetLifespanLossrate()
    {
        return GetHappinessLifespanLossModifier() * GetHungerLifespanLossModifier() * lifespanLossRate;
    }

    void UpdateLifespan()
    {
        currentAge += Time.deltaTime;
        lifespan -= Time.deltaTime * GetLifespanLossrate();

        if (lifespan <= 0)
        {
            SetLifeStage(CharacterLifeStage.DEAD);
            EventsManager.instance.FireDeathEvent(currentAge);
            return;
        }

        //Is there a better place for this?
        if (LifespanRelativeAge() > babyRelativeAge)
        {
            if(LifespanRelativeAge() > OldAgeRelativeAge)
            {
                SetLifeStage(CharacterLifeStage.OLD_AGE);
            } else
            {
                SetLifeStage(CharacterLifeStage.ADULT);
            }
        } else
        {
            SetLifeStage(CharacterLifeStage.BABY);
        }
    }

    void SetLifeStage(CharacterLifeStage newLifeStage)
    {
        if (newLifeStage == currentLifeStage) return;

        currentLifeStage = newLifeStage;

        switch (currentLifeStage)
        {
            case CharacterLifeStage.BABY:
                movementSpeedScaler = speedScaleBaby;
                break;
            case CharacterLifeStage.ADULT:
                movementSpeedScaler = speedScaleAdult;
                break;
            case CharacterLifeStage.OLD_AGE:
                movementSpeedScaler = speedScaleAged;
                break;
            case CharacterLifeStage.DEAD:
                movementSpeedScaler = 0f;
                break;
            default:
                break;
        }

        EventsManager.instance.FireChangedLifeStageEvent(currentLifeStage);
    }

    void UpdateStats()
    {
        //Routine stat update
        foodEnergy -= Time.deltaTime * hungerRate;
        happiness -= Time.deltaTime * sadnessRate;

        if (foodEnergy < 0)
        {
            foodEnergy = 0;
            EventsManager.instance.FireFoodEnergyBelowZeroEvent();
        }
        EventsManager.instance.FireCharacterStatChangedEvent(CharacterStatType.HUNGER, foodEnergy);

        if (happiness < 0)
        {
            happiness = 0;
            EventsManager.instance.FireHappinessBelowZeroEvent();
        }
        EventsManager.instance.FireCharacterStatChangedEvent(CharacterStatType.HAPPINESS, happiness);
    }

    void AddWorkRate(float amt)
    {
        workRate += amt;
    }

    void AddHappiness(float amt)
    {
        happiness += amt;
        if (happiness > 100) happiness = 100;

        EventsManager.instance.FireCharacterStatChangedEvent(CharacterStatType.HAPPINESS, happiness);
    }

    void AddFood(float amt)
    {
        foodEnergy += amt;
        if (foodEnergy > 100) foodEnergy = 100;

        EventsManager.instance.FireCharacterStatChangedEvent(CharacterStatType.HUNGER, foodEnergy);
    }

    void AddResources(float amt)
    {
        resources += amt;

        EventsManager.instance.FireCharacterStatChangedEvent(CharacterStatType.RESOURCES, resources);
    }

    public float WorkRate()
    {
        return workRate;
    }

    public float LifespanRelativeAge()
    {
        return (maxLifespan - lifespan) / maxLifespan;
    }

    public void ForceDeath()
    {
        lifespan = 0;
    }
}
