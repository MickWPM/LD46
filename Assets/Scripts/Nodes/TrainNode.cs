using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainNode : MonoBehaviour, IClickable
{
    public float remaining = 0.1f;
    public float harvestRate = 0.01f;

    public bool TryAction(float workRate)
    {
        float qty = Time.deltaTime * workRate * harvestRate;
        remaining -= qty;

        EventsManager.instance.FireChangeWorkRateEvent(qty);

        if (remaining <= 0)
        {
            EventsManager.instance.FireWorkRateNodeExhaustedEvent(transform.position);
            Destroy(gameObject);
        }

        return remaining > 0;
    }

    public MouseHoverCategories GetClickableCategory()
    {
        return MouseHoverCategories.TRAIN;
    }
}