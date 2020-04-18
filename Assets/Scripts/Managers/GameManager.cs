using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public CharacterStats characterStats;

    private void Awake()
    {
        instance = this;
        if (characterStats == null) characterStats = FindObjectOfType<CharacterStats>();
    }

    private void Start()
    {
        
    }


}
