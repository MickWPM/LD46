using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEgg : MonoBehaviour
{
    public SpriteRenderer sr;
    public Sprite[] crackSprites;
    private void Start()
    {
        EventsManager.instance.CharacterEggClickedEvent += EggClicked;
    }

    public int cracks = 0;
    void EggClicked()
    {
        ++cracks;
        if (cracks > crackSprites.Length)
        {
            Hatch();
        } else
        {
            sr.sprite = crackSprites[cracks-1];
        }
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
