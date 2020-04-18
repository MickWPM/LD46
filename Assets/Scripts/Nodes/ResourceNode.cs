using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceNode : MonoBehaviour, IClickable
{
    public float remaining = 100;

    public bool TryAction(float workRate)
    {
        float qty = Time.deltaTime * workRate;
        remaining -= qty;

        EventsManager.instance.FireChangeResourcesEvent(qty);

        if (remaining <= 0)
        {
            EventsManager.instance.FireResourceNodeExhaustedEvent(transform.position);
            Destroy(gameObject);
        }

        return remaining > 0;
    }

}
