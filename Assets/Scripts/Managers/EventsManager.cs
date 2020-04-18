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

    public event System.Action PatThePetEvent;
    public void FirePatThePetEvent()
    {
        PatThePetEvent?.Invoke();
    }

    #region UIEvents



    #endregion


    #region CharacterEvents

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

    #endregion

    #region NodeEvents

    public event System.Action<Nodes> NodeSpawnedEvent;
    internal void FireNodeSpawnedEvent(Nodes node)
    {
        NodeSpawnedEvent?.Invoke(node);
    }

    public event System.Action<float> WorkRateChangedByTrainNodeEvent;
    internal void FireChangeWorkRateByTrainNodeEvent(float amount)
    {
        WorkRateChangedByTrainNodeEvent?.Invoke(amount);
    }


    public event System.Action<float> ResourcesChangedByNodeEvent;
    public void FireChangeResourcesByNodeEvent(float amount)
    {
        ResourcesChangedByNodeEvent?.Invoke(amount);
    }

    public event System.Action<float> FoodAddedByNodeEvent;
    internal void FireAddFoodByNodeEvent(float amount)
    {
        FoodAddedByNodeEvent?.Invoke(amount);
    }


    public event System.Action<float> HappinessAddedByNodeEvent;
    public void FireAddHappinessByNodeEvent(float amount)
    {
        HappinessAddedByNodeEvent?.Invoke(amount);
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

#endregion


}
