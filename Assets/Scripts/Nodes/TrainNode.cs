using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainNode : MonoBehaviour, IClickable
{
    public float remaining = 0.1f;
    public float harvestRate = 0.01f;
    public float decayRate = 0.0005f;

    float initialQty;
    private void Start()
    {
        initialQty = remaining;
    }

    void Update()
    {
        remaining -= Time.deltaTime * decayRate;
        if (remaining <= 0)
        {
            EventsManager.instance.FireWorkRateNodeExhaustedEvent(transform.position);
            Destroy(gameObject);
        }
    }


    public bool TryAction(float workRate)
    {
        float qty = Time.deltaTime * workRate * harvestRate;
        remaining -= qty;

        EventsManager.instance.FireChangeWorkRateByTrainNodeEvent(qty);

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

    public float GetPercentRemaining()
    {
        return 100f * remaining / initialQty;
    }

    public string GetDescription()
    {
        return "Train";
    }
}