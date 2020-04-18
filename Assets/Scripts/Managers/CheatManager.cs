using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheatManager : MonoBehaviour
{
    public KeyCode KillPlayer = KeyCode.D;
    public KeyCode GiveResources = KeyCode.M;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KillPlayer))
        {
            CharacterStats stats = FindObjectOfType<CharacterStats>();
            if (stats == null)
                return;
            stats.ForceDeath();
        }

        if (Input.GetKeyDown(GiveResources))
        {
            CharacterStats stats = FindObjectOfType<CharacterStats>();
            if (stats == null)
                return;
            stats.CheatResources();
        }
    }
}
