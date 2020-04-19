using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum Nodes { FOOD, WORK, PLAY, TRAIN };
public class SpawningManager : MonoBehaviour
{
    public FoodNode foodNodePrefab;
    public ResourceNode workNodePrefab;
    public PlayNode playNodePrefab;
    public TrainNode trainNodePrefab;

    public RectTransform gameArea;

    Vector2 GetTopRightGameAreaWorldSpace(float xOffset = 0, float yOffset = 0)
    {
        Vector2 topLeft = new Vector2(-gameArea.sizeDelta.x + xOffset, Screen.height + gameArea.sizeDelta.y - yOffset);
        Debug.Log($"Topleft = {topLeft}");
        return Camera.main.ScreenToWorldPoint(topLeft);
    }



    Vector3 GetNewSpawnPos()
    {
        //Dont use the FULL edge of the screen - leave room for sprite size
        float xOffset = Screen.width * 0.05f;
        float yOffset = Screen.height * 0.05f;
        Vector2 originToWorld = GetTopRightGameAreaWorldSpace(xOffset, yOffset);
        Vector2 screenEdgeToWorld = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width - xOffset, yOffset));

        //TODO: Physics2D.OverlapCollider for all nodes in the scene to make sure it doesnt double up?

        return new Vector3(
            Random.Range(originToWorld.x, screenEdgeToWorld.x),
            Random.Range(originToWorld.y, screenEdgeToWorld.y),
            0);
    }
    
    bool CanSpawn(Nodes node)
    {
        return GameManager.instance.CanAfford(node);
    }

    public void SpawnFoodNode()
    {
        if (!CanSpawn(Nodes.FOOD)) return;
        Instantiate(foodNodePrefab, GetNewSpawnPos(), Quaternion.identity);

        EventsManager.instance.FireNodeSpawnedEvent(Nodes.FOOD);
    }

    public void SpawnWorkNode()
    {
        if (!CanSpawn(Nodes.WORK)) return;
        Instantiate(workNodePrefab, GetNewSpawnPos(), Quaternion.identity);

        EventsManager.instance.FireNodeSpawnedEvent(Nodes.WORK);
    }

    public void SpawnPlayNode()
    {
        if (!CanSpawn(Nodes.PLAY)) return;
        Instantiate(playNodePrefab, GetNewSpawnPos(), Quaternion.identity);

        EventsManager.instance.FireNodeSpawnedEvent(Nodes.PLAY);
    }

    public void SpawnTrainNode()
    {
        if (!CanSpawn(Nodes.TRAIN)) return;
        Instantiate(trainNodePrefab, GetNewSpawnPos(), Quaternion.identity);

        EventsManager.instance.FireNodeSpawnedEvent(Nodes.TRAIN);
    }
}
