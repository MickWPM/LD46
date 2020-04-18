﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodNode : MonoBehaviour, IClickable
{
    public float remaining = 10;
    public float harvestRate = 2.5f;

    public bool TryAction(float workRate)
    {
        float qty = Time.deltaTime * harvestRate * workRate;
        remaining -= qty;

        EventsManager.instance.FireAddFoodEvent(qty);

        if (remaining <= 0)
        {
            EventsManager.instance.FireFoodNodeExhaustedEvent(transform.position);
            Destroy(gameObject);
        }

        return remaining > 0;
    }
}
