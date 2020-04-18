using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    CharacterStats characterStats;


    private void Awake()
    {
        instance = this;
        if (characterStats == null) characterStats = FindObjectOfType<CharacterStats>();
    }

    private void Start()
    {
        EventsManager.instance.CharacterDeathEvent += (float age) => 
        {
            characterStats.gameObject.SetActive(false);
            Debug.Log($"Died after {age} seconds");
        };
    }


}
