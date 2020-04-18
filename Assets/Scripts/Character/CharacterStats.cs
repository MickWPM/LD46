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

    public float resources = 0;

    float currentAge = 0;

    private void Start()
    {
        maxLifespan = lifespan;
        EventsManager.instance.FoodAddedEvent += AddFood;
        EventsManager.instance.HappinessAddedEvent += AddHappiness;
        EventsManager.instance.ResourcesChangedEvent += AddResources;
        EventsManager.instance.WorkRateChangedEvent += AddWorkRate;
    }

    public UnityEngine.UI.Text debugStatText;
    private void Update()
    {
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
