using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_InteractionPanel : MonoBehaviour
{
    public RectTransform uiInteractionPanel;
    public bool showUI = false;

    private void Update()
    {
        if(Input.GetMouseButtonDown(1))
        {
            showUI = !showUI;
        }

        if (showUI)
        {
            DoShowUI();
        } else
        {
            DoHideUI();
        }

    }

    float hiddenX = -400, shownX = 0;
    float uiShownPercent = 1;
    float slideTime = .2f;
    void DoShowUI()
    {
        if (uiShownPercent < 1)
        {
            uiShownPercent += Time.deltaTime / slideTime;
            float newX = Mathf.Lerp(hiddenX, shownX, uiShownPercent);
            uiInteractionPanel.anchoredPosition = new Vector2(newX, 0);
        }

    }

    void DoHideUI()
    {
        if (uiShownPercent > 0)
        {
            uiShownPercent -= Time.deltaTime / slideTime;
            float newX = Mathf.Lerp(hiddenX, shownX, uiShownPercent);
            uiInteractionPanel.anchoredPosition = new Vector2(newX, 0);
        }
    }

    public void SetUIShown(bool show)
    {
        showUI = show;
    }

}
