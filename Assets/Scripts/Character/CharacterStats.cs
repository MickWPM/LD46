using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStats : MonoBehaviour
{
    [SerializeField] float lifespan = 300;
    [SerializeField] float foodEnergy = 100;
    [SerializeField] float hungerRate = 100f/30f;
    [SerializeField] float happiness = 100;
    [SerializeField] float sadnessRate = 100f/120f;
    [SerializeField] float workRate = 1.0f; //Change with age?

    public float resources = 0;

    float currentAge = 0;

    private void Start()
    {
        EventsManager.instance.FoodAddedEvent += AddFood;
        EventsManager.instance.HappinessAddedEvent += AddHappiness;
        EventsManager.instance.ResourcesChangedEvent += AddResources;
    }

    private void Update()
    {
        UpdateStats();
    }

    void UpdateStats()
    {
        //Routine stat update
        lifespan -= Time.deltaTime;
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
        return currentAge / lifespan;
    }
}
