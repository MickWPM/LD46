using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceNode : MonoBehaviour, IClickable
{
    public float qty = 100;
    public bool TryAction()
    {
        qty -= Time.deltaTime;

        if (qty <= 0) Destroy(gameObject);

        return qty > 100;
    }

    void Update()
    {
        
    }
}
