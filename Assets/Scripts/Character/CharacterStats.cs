using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CharacterLifeStage
{
    BABY,
    ADULT,
    OLD_AGE,
    DEAD
}


public class CharacterStats : MonoBehaviour
{
    [SerializeField] CharacterLifeStage currentLifeStage = CharacterLifeStage.BABY;
    [SerializeField] bool degradeWithTime = false;
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
    public float resources = 0;

    float currentAge = 0;
    float timeSincePatThePet = 0;

    private void Start()
    {
        movementSpeedScaler = speedScaleBaby;
        maxLifespan = lifespan;
        EventsManager.instance.FoodAddedByNodeEvent += FoodNode;
        EventsManager.instance.HappinessAddedByNodeEvent += HappinessNode;
        EventsManager.instance.ResourcesChangedByNodeEvent += ResourcesNode;
        EventsManager.instance.WorkRateChangedByTrainNodeEvent += TrainNode;
        EventsManager.instance.PatThePetEvent += PatThePet;
    }

    float foodRemovedPerWorkSecond = 2, happinessRemovedPerWorkSecond = 2;
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

    float foodRemovedPerTrainSecond = 1, happinessRemovedPerTrainSecond = 4;
    void TrainNode(float amt)
    {
        AddWorkRate(amt);
        //Remove happiness and food for this tick
        AddFood(Time.deltaTime * -foodRemovedPerTrainSecond);
        AddHappiness(Time.deltaTime * -happinessRemovedPerTrainSecond);
    }


    public UnityEngine.UI.Text debugStatText;
    private void Update()
    {
        timeSincePatThePet += Time.deltaTime;
        if (degradeWithTime) UpdateStats();

        UpdateLifespan();

        string s = "DEBUG INFO - STATS:\n";
        s += "\nCurrent age: " + currentAge;
        s += "\nCurrent life stage: " + currentLifeStage;
        s += "\nCurrent Food satisfaction: " + foodEnergy;
        s += "\nCurrent Happiness: " + happiness;
        s += "\nCurrent resources: " + resources;

        s += "\n\nCurrent hunger rate: " + hungerRate;
        s += "\n\nCurrent sadness rate: " + sadnessRate;
        s += "\n\nCurrent work rate: " + workRate;
        s += "\n\nCurrent lifespan loss rate: " + lifespanLossRate;


        debugStatText.text = s;
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

    float babyRelativeAge = 0.15f;
    float OldAgeRelativeAge = 0.6f;


    void UpdateLifespan()
    {
        currentAge += Time.deltaTime;
        lifespan -= Time.deltaTime * lifespanLossRate;

        if (lifespan <= 0)
        {
            SetLifeStage(CharacterLifeStage.DEAD);
            EventsManager.instance.FireDeathEvent(currentAge);
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

        if (happiness < 0)
        {
            happiness = 0;
            EventsManager.instance.FireHapinessBelowZeroEvent();
        }
    }

    void AddWorkRate(float amt)
    {
        workRate += amt;
    }

    void AddHappiness(float amt)
    {
        happiness += amt;
        if (happiness > 100) happiness = 100;
    }

    void AddFood(float amt)
    {
        foodEnergy += amt;
        if (foodEnergy > 100) foodEnergy = 100;
    }

    void AddResources(float amt)
    {
        resources += amt;
    }

    public float WorkRate()
    {
        return workRate;
    }

    public float LifespanRelativeAge()
    {
        return currentAge / maxLifespan;
    }
}
