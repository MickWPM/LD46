using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceNode : MonoBehaviour, IClickable
{
    public float remaining = 100;
    public float harvestRate = 1f;

    public bool TryAction(float workRate)
    {
        float qty = Time.deltaTime * workRate * harvestRate;
        remaining -= qty;

        EventsManager.instance.FireChangeResourcesByNodeEvent(qty);

        if (remaining <= 0)
        {
            EventsManager.instance.FireResourceNodeExhaustedEvent(transform.position);
            Destroy(gameObject);
        }

        return remaining > 0;
    }

    public MouseHoverCategories GetClickableCategory()
    {
        return MouseHoverCategories.RESOURCE;
    }
}
