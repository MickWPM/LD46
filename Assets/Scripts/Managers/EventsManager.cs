using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventsManager : MonoBehaviour
{
    public static EventsManager instance;
    private void Awake()
    {
        instance = this;
    }

    public event System.Action<MouseHoverCategories> MouseHoverObjectUpdatedEvent;
    public void FireMouseHoverObjectUpdatedEvent(MouseHoverCategories hoverObjectCategory)
    {
        MouseHoverObjectUpdatedEvent?.Invoke(hoverObjectCategory);
    }

    public event System.Action<float> CharacterDeathEvent;
    internal void FireDeathEvent(float currentAge)
    {
        CharacterDeathEvent?.Invoke(currentAge);
    }


    public event System.Action<CharacterLifeStage> CharacterLifeStageChangedEvent;
    internal void FireChangedLifeStageEvent(CharacterLifeStage currentLifeStage)
    {
        CharacterLifeStageChangedEvent?.Invoke(currentLifeStage);
    }


    public event System.Action<float> WorkRateChangedEvent;
    internal void FireChangeWorkRateEvent(float amount)
    {
        WorkRateChangedEvent?.Invoke(amount);
    }

    public event System.Action<float> ResourcesChangedEvent;
    public void FireChangeResourcesEvent(float amount)
    {
        ResourcesChangedEvent?.Invoke(amount);
    }

    public event System.Action<float> FoodAddedEvent;
    internal void FireAddFoodEvent(float amount)
    {
        FoodAddedEvent?.Invoke(amount);
    }


    public event System.Action<float> HappinessAddedEvent;
    public void FireAddHappinessEvent(float amount)
    {
        HappinessAddedEvent?.Invoke(amount);
    }

    public event System.Action HappinessBelowZeroEvent;
    internal void FireHapinessBelowZeroEvent()
    {
        HappinessBelowZeroEvent?.Invoke();
    }

    public event System.Action FoodEnergyBelowZeroEvent;
    internal void FireFoodEnergyBelowZeroEvent()
    {
        FoodEnergyBelowZeroEvent?.Invoke();
    }
    

    public event System.Action<Vector3> ResourceNodeExhaustedEvent;
    internal void FireResourceNodeExhaustedEvent(Vector3 position)
    {
        ResourceNodeExhaustedEvent?.Invoke(position);
    }

    public event System.Action<Vector3> PlayNodeExhaustedEvent;

    internal void FirePlayNodeExhaustedEvent(Vector3 position)
    {
        PlayNodeExhaustedEvent?.Invoke(position);
    }


    public event System.Action<Vector3> FoodNodeExhaustedEvent;
    internal void FireFoodNodeExhaustedEvent(Vector3 position)
    {
        FoodNodeExhaustedEvent?.Invoke(position);
    }


    public event System.Action<Vector3> WorkRateNodeExhaustedEvent;
    internal void FireWorkRateNodeExhaustedEvent(Vector3 position)
    {
        WorkRateNodeExhaustedEvent?.Invoke(position);
    }

}
