using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEgg : MonoBehaviour
{
    private void Start()
    {
        EventsManager.instance.CharacterEggClickedEvent += EggClicked;
    }

    public int cracksRemaining = 3;
    void EggClicked()
    {
        --cracksRemaining;
        Debug.Log("Crakcing sound");
        if (cracksRemaining == 0)
            Hatch();
    }

    void Hatch()
    {
        EventsManager.instance.FireEggHatchedEvent(transform.position);
        Destroy(gameObject);
    }

    private void OnDestroy()
    {
        EventsManager.instance.CharacterEggClickedEvent -= EggClicked;
    }
}
