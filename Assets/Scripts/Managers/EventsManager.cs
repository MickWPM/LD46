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

}
