using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnTest : MonoBehaviour
{
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

    public GameObject prefabToSpawn;
    void Update()
    {
        
        if(Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log(Input.mousePosition);
            Instantiate(prefabToSpawn, GetNewSpawnPos(), Quaternion.identity);
        }
    }
}
