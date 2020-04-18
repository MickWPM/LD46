using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MouseManager : MonoBehaviour
{
    [SerializeField]
    CharacterController timTamController;

    public MouseHoverCategories currentHover;

    private void Awake()
    {
        currentHover = MouseHoverCategories.NULL;
        Cursor.SetCursor(defaultCursor, Vector2.zero, CursorMode.ForceSoftware);

    }

    private void Start()
    {
        EventsManager.instance.MouseHoverObjectUpdatedEvent += UpdateMouseCursor;
        EventsManager.instance.PlayerSpawnedEvent += (CharacterStats stats) => { timTamController = stats.gameObject.GetComponent<CharacterController>(); };
    }

    public Texture2D resourceCursor, foodCursor, playCursor, trainCursor, defaultCursor, patThePetCursor, eggCrackCursor;
    void UpdateMouseCursor(MouseHoverCategories category)
    {
        switch (category)
        {
            case MouseHoverCategories.RESOURCE:
                Cursor.SetCursor(resourceCursor, Vector2.zero, CursorMode.ForceSoftware);
                break;
            case MouseHoverCategories.FOOD:
                Cursor.SetCursor(foodCursor, Vector2.zero, CursorMode.ForceSoftware);
                break;
            case MouseHoverCategories.PLAY:
                Cursor.SetCursor(playCursor, Vector2.zero, CursorMode.ForceSoftware);
                break;
            case MouseHoverCategories.TRAIN:
                Cursor.SetCursor(trainCursor, Vector2.zero, CursorMode.ForceSoftware);
                break;
            case MouseHoverCategories.ME:
                Cursor.SetCursor(patThePetCursor, Vector2.zero, CursorMode.ForceSoftware);
                break;
            case MouseHoverCategories.EGG:
                Cursor.SetCursor(eggCrackCursor, Vector2.zero, CursorMode.ForceSoftware);
                break;
            case MouseHoverCategories.NULL:
            case MouseHoverCategories.GROUND:
            default:
                Cursor.SetCursor(defaultCursor, Vector2.zero, CursorMode.ForceSoftware);
                break;
        }
    }

    void Update()
    {
        if (UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject())
        {
            SetCurrentHover(MouseHoverCategories.NULL);
        } else
        {
            UpdateMouseHover();
        }


        if (Input.GetMouseButtonDown(0))
        {
            switch (currentHover)
            {
                case MouseHoverCategories.RESOURCE:
                    EventsManager.instance.FireWorkNodeClickedEvent();
                    timTamController.ClickedObject(hoverObject);
                    break;
                case MouseHoverCategories.FOOD:
                    EventsManager.instance.FireFoodNodeClickedEvent();
                    timTamController.ClickedObject(hoverObject);
                    break;
                case MouseHoverCategories.PLAY:
                    EventsManager.instance.FirePlayNodeClickedEvent();
                    timTamController.ClickedObject(hoverObject);
                    break;
                case MouseHoverCategories.TRAIN:
                    EventsManager.instance.FireTrainNodeClickedEvent();
                    timTamController.ClickedObject(hoverObject);
                    break;
                case MouseHoverCategories.ME:
                    EventsManager.instance.FirePatThePetEvent();
                    break;
                case MouseHoverCategories.EGG:
                    EventsManager.instance.FireEggClickedEvent();
                    break;
                case MouseHoverCategories.NULL:
                case MouseHoverCategories.GROUND:
                default:
                    break;
            }                
        }
    }


    public GameObject hoverObject;
    void UpdateMouseHover()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);

        hoverObject = hit ? hit.collider.gameObject : null;

        MouseHoverCategories newHoverCat = MouseHoverCategories.NULL;
        if(hoverObject != null)
        {
            IClickable clickable = hoverObject.GetComponent<IClickable>();
            if (clickable != null)
            {
                newHoverCat = clickable.GetClickableCategory();
            } else
            {
                CharacterStats stats = hoverObject.GetComponent<CharacterStats>();
                if (stats != null)
                {
                    newHoverCat = MouseHoverCategories.ME;
                } else
                {
                    PlayerEgg egg = hoverObject.GetComponent<PlayerEgg>();
                    if (egg != null)
                    {
                        newHoverCat = MouseHoverCategories.EGG;
                    } else
                    {
                        //Only other option initially is ground.
                        //This may need to be udpated later
                        newHoverCat = MouseHoverCategories.GROUND;
                    }
                }
            }
        }

        SetCurrentHover(newHoverCat);

    }

    void SetCurrentHover(MouseHoverCategories newHoverCat)
    {
        if (currentHover != newHoverCat)
        {
            currentHover = newHoverCat;
            EventsManager.instance.FireMouseHoverObjectUpdatedEvent(currentHover);
        }
    }

}

public enum MouseHoverCategories
{
    NULL,
    GROUND,
    ME,
    RESOURCE,
    FOOD,
    PLAY,
    TRAIN,
    EGG
}