using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseManager : MonoBehaviour
{
    [SerializeField]
    CharacterController timTam;

    void Update()
    {
        UpdateMouseHover();

        if (Input.GetMouseButtonDown(0))
        {
            if (hoverObject != null)
                timTam.ClickedObject(hoverObject);
        }
    }


    public GameObject hoverObject;
    void UpdateMouseHover()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);

        hoverObject = hit ? hit.collider.gameObject : null;
    }

}
