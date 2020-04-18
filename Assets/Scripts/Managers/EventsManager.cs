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

    public event System.Action EndTutorialEvent;
    internal void FireEndTutorialEvent()
    {
        EndTutorialEvent?.Invoke();
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
    

    public event System.Action<CharacterHungerState, CharacterHungerState> ChangeHungerStateEvent;
    internal void FireChangeHungerStateEvent(CharacterHungerState currentHungerState, CharacterHungerState newHungerState)
    {
        ChangeHungerStateEvent?.Invoke(currentHungerState, newHungerState);
    }

    public event System.Action<CharacterHappinessState, CharacterHappinessState> ChangeHappinessStateEvent;
    internal void FireChangeHappinessStateEvent(CharacterHappinessState currentHappinessState, CharacterHappinessState newHappinessState)
    {
        ChangeHappinessStateEvent?.Invoke(currentHappinessState, newHappinessState);
    }

    public event System.Action CharacterEggClickedEvent;
    internal void FireEggClickedEvent()
    {
        CharacterEggClickedEvent?.Invoke();
    }

    public event System.Action<Vector3> CharacterEggHatchedEvent;
    internal void FireEggHatchedEvent(Vector3 hatchLocation)
    {
        CharacterEggHatchedEvent?.Invoke(hatchLocation);
    }

    public event System.Action<CharacterStats> PlayerSpawnedEvent;
    internal void FirePlayerSpawnedEvent(CharacterStats timtam)
    {
        PlayerSpawnedEvent?.Invoke(timtam);
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

    #endregion

    #region NodeEvents

    public event System.Action PlayNodeClickedEvent;
    internal void FirePlayNodeClickedEvent()
    {
        PlayNodeClickedEvent?.Invoke();
    }
    public event System.Action WorkNodeClickedEvent;
    internal void FireWorkNodeClickedEvent()
    {
        WorkNodeClickedEvent?.Invoke();
    }
    public event System.Action TrainNodeClickedEvent;
    internal void FireTrainNodeClickedEvent()
    {
        TrainNodeClickedEvent?.Invoke();
    }
    public event System.Action FoodNodeClickedEvent;
    internal void FireFoodNodeClickedEvent()
    {
        FoodNodeClickedEvent?.Invoke();
    }


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
