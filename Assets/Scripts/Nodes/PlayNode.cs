using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayNode : MonoBehaviour, IClickable
{
    public float remaining = 10;
    public float harvestRate = 0.1f;

    public bool TryAction(float workRate)
    {
        float qty = Time.deltaTime * harvestRate * workRate;
        remaining -= qty;

        EventsManager.instance.FireAddHappinessEvent(qty);

        if (remaining <= 0)
        {
            EventsManager.instance.FirePlayNodeExhaustedEvent(transform.position);
            Destroy(gameObject);
        }

        return remaining > 0;
    }

}
