using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartScreenManager : MonoBehaviour
{
    public float musicStartTime = 2f;
    public float mainScreenActivationTime = 3.5f;
    public float totalDelayTime = 4.5f;
    public GameObject mainScreenObject;
    public GameObject musicPlayerObject;
    public GameObject clickToContinue;

    private void Start()
    {
        StartCoroutine(StartMusic());
        StartCoroutine(StartMainScreen());
        StartCoroutine(AllowGameStart());
    }

    IEnumerator StartMusic()
    {
        yield return new WaitForSeconds(musicStartTime);
        musicPlayerObject.SetActive(true);
    }

    IEnumerator StartMainScreen()
    {
        yield return new WaitForSeconds(mainScreenActivationTime);
        mainScreenObject.SetActive(true);
    }
    IEnumerator AllowGameStart()
    {
        yield return new WaitForSeconds(totalDelayTime);
        clickToContinue.SetActive(true);
        canStart = true;
    }

    bool canStart = false;
    private void Update()
    {
        if (!canStart) return;


        if (Input.anyKey)
        {
            canStart = false;
            //LOAD GAME SCENE
            UnityEngine.SceneManagement.SceneManager.LoadScene(1);
        }
    }

}
