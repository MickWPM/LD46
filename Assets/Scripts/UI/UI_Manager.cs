using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Manager : MonoBehaviour
{
    [SerializeField] Button buyFoodButton, buyHappinessButton, buyWorkButton, buyTrainingButton;
    public Text hoverPopupText;
    public GameObject hoverPopupParent;
    private void Awake()
    {
        buyFoodButton.interactable = false;
        buyHappinessButton.interactable = false;
        buyWorkButton.interactable = false;
        buyTrainingButton.interactable = false;

        hoverPopupText.text = "";
        hoverPopupParent.SetActive(false);
    }

    void Start()
    {
        EventsManager.instance.CharacterDeathEvent += OnCharacterDied;
        EventsManager.instance.CharacterStatChangedEvent += OnStatUpdate;
        EventsManager.instance.MouseHoverObjectUpdatedEvent += OnMouseHoverObjectChange;
        EventsManager.instance.EndTutorialEvent += () => { OnResourcesUpdated(0); };
    }

    private void Update()
    {
        if (clickableHover != null)
            UpdateClickablePopup();
    }

    void UpdateClickablePopup()
    {
        hoverPopupText.text = $"Node: Training.\nPercent remaining: {Mathf.RoundToInt(clickableHover.GetPercentRemaining())}%";
        hoverPopupParent.SetActive(true);
    }

    IClickable clickableHover;
    private void OnMouseHoverObjectChange(MouseHoverCategories hoverCategory, GameObject hoverObject)
    {
        clickableHover = null;
        switch (hoverCategory)
        {
            case MouseHoverCategories.ME:
                hoverPopupText.text = "What a cute little pet";
                hoverPopupParent.SetActive(true);
                break;
            case MouseHoverCategories.RESOURCE:
            case MouseHoverCategories.FOOD:
            case MouseHoverCategories.PLAY:
            case MouseHoverCategories.TRAIN:
                IClickable clickable = hoverObject.GetComponent<IClickable>();
                if (clickable == null)
                {
                    Debug.LogError("Clickable object has no clickable component", hoverObject);
                    hoverPopupText.text = "";
                    hoverPopupParent.SetActive(false);
                } else
                {
                    clickableHover = clickable;
                    UpdateClickablePopup();
                }
                break;
            case MouseHoverCategories.EGG:
            case MouseHoverCategories.NULL:
            case MouseHoverCategories.GROUND:
            default:
                hoverPopupText.text = "";
                hoverPopupParent.SetActive(false);
                break;
        }
    }

    public Image hungerProgressImage, happinessProgressImage;
    void OnStatUpdate(CharacterStatType stat, float value)
    {
        switch (stat)
        {
            case CharacterStatType.RESOURCES:
                OnResourcesUpdated(value);
                break;
            case CharacterStatType.HUNGER:
                OnProgressStatUpdate(hungerProgressImage, value / 100f);
                break;
            case CharacterStatType.HAPPINESS:
                OnProgressStatUpdate(happinessProgressImage, value / 100f);
                break;
            default:
                break;
        }
    }

    void OnProgressStatUpdate(Image fillImage, float value)
    {
        fillImage.fillAmount = value;
    }

    public Text resourcesText;
    void OnResourcesUpdated(float value)
    {
        buyFoodButton.interactable = GameManager.instance.CanAfford(Nodes.FOOD);
        buyHappinessButton.interactable = GameManager.instance.CanAfford(Nodes.PLAY);
        buyWorkButton.interactable = GameManager.instance.CanAfford(Nodes.WORK);
        buyTrainingButton.interactable = GameManager.instance.CanAfford(Nodes.TRAIN);

        resourcesText.text = Mathf.RoundToInt(value).ToString();
    }


    public GameObject gameOverScreen;
    public Text gameOverText;
    void OnCharacterDied(float lifetime)
    {
        string s = $"Well.............. Remind me NOT to let you look after my pet.\n\n\n\nYou managed to keep this poor little creature alive for {lifetime} days until it died of old age.";
        gameOverText.text = s;
        gameOverScreen.SetActive(true);
    }

    public void RestartButtonPressed()
    {
        gameOverScreen.SetActive(false);
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
    }
}
