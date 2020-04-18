using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheatManager : MonoBehaviour
{
    public KeyCode KillPlayer = KeyCode.D;

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KillPlayer))
        {
            CharacterStats stats = FindObjectOfType<CharacterStats>();
            if (stats == null)
                return;
            stats.ForceDeath();
        }
    }
}
