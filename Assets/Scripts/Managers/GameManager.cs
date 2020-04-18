using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [SerializeField] float foodNodeCost;
    [SerializeField] float workNodeCost;
    [SerializeField] float trainNodeCost;
    [SerializeField] float playNodeCost;

    CharacterStats characterStats;

    public float PlayNodeCost { get => playNodeCost; private set => playNodeCost = value; }
    public float TrainNodeCost { get => trainNodeCost; private set => trainNodeCost = value; }
    public float WorkNodeCost { get => workNodeCost; private set => workNodeCost = value; }
    public float FoodNodeCost { get => foodNodeCost; private set => foodNodeCost = value; }

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

    internal float NodeCost(Nodes node)
    {
        float cost = 99999999;
        switch (node)
        {
            case Nodes.FOOD:
                cost = GameManager.instance.FoodNodeCost;
                break;
            case Nodes.WORK:
                cost = GameManager.instance.WorkNodeCost;
                break;
            case Nodes.PLAY:
                cost = GameManager.instance.PlayNodeCost;
                break;
            case Nodes.TRAIN:
                cost = GameManager.instance.TrainNodeCost;
                break;
            default:
                break;
        }

        return cost;
    }

    internal bool CanAfford(Nodes node)
    {


        return NodeCost(node) <= characterStats.Resources;
    }
}
