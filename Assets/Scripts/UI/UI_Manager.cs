using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Manager : MonoBehaviour
{
    public Image musicToggleImage;
    public Sprite musicOnSprite, musicOffSprite;
    MusicManager musicManager;

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

    public MusicManager musicManagerBackupPrefab;
    void Start()
    {
        EventsManager.instance.CharacterDeathEvent += OnCharacterDied;
        EventsManager.instance.CharacterStatChangedEvent += OnStatUpdate;
        EventsManager.instance.MouseHoverObjectUpdatedEvent += OnMouseHoverObjectChange;
        EventsManager.instance.EndTutorialEvent += () => { OnResourcesUpdated(0); };
        EventsManager.instance.NodeSpawnedEvent += CheckFailStateNodePlaced;
        EventsManager.instance.ResourceNodeExhaustedEvent += CheckFailStateWorkNodeExpired;
        EventsManager.instance.CharacterStatChangedEvent += CheckFailStateStatChanged;

        musicManager = GameObject.FindObjectOfType<MusicManager>();
        if (musicManager == null)
        {
            musicManager = Instantiate(musicManagerBackupPrefab);
            musicManager.gameObject.SetActive(true);
        }

        buyFoodButton.interactable = false;
        buyHappinessButton.interactable = false;
        buyWorkButton.interactable = false;
        buyTrainingButton.interactable = false;
        musicToggleImage.sprite = musicManager.IsMusicOn() ? musicOnSprite : musicOffSprite;
        failStateGameObject.SetActive(false);
    }

    private void Update()
    {
        if (clickableHover != null)
            UpdateClickablePopup();
    }

    public void ToggleMusic()
    {
        bool musicOn = musicManager.ToggleMusicNowOn();
        musicToggleImage.sprite = musicOn ? musicOnSprite : musicOffSprite;
    }



    private void CheckFailStateStatChanged(CharacterStatType stat, float value)
    {
        if (stat != CharacterStatType.RESOURCES)
            return;

        CheckFailState();
    }


    void CheckFailStateWorkNodeExpired(Vector3 loc)
    {
        CheckFailState();
    }

    void CheckFailStateNodePlaced(Nodes node)
    {
        //If we just replaced a node, we are fine
        if (node == Nodes.WORK)
            return;
        CheckFailState();
    }

    public GameObject failStateGameObject;
    void CheckFailState()
    {
        ResourceNode[] workNodes = GameObject.FindObjectsOfType<ResourceNode>();
        if (workNodes.Length > 0)
        {
            for (int i = 0; i < workNodes.Length; i++)
            {
                ResourceNode thisNode = workNodes[i];
                if (thisNode.GetPercentRemaining() > 0)
                    return;
            }

        }
            
        if (GameManager.instance.CanAfford(Nodes.WORK))
            return;

        //At this point - we cant afford any more work nodes and none are left!
        Debug.Log("FAIL CONDITION");
        failStateGameObject.SetActive(true);
    }

    void UpdateClickablePopup()
    {
        hoverPopupText.text = $"Node: {clickableHover.GetDescription()}.\nPercent remaining: {Mathf.RoundToInt(clickableHover.GetPercentRemaining())}%";
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

        resourcesText.text = Mathf.FloorToInt(value).ToString();
    }


    public GameObject gameOverScreen;
    public Text gameOverText;
    void OnCharacterDied(float lifetime)
    {
        string s = $"Well.............. Remind me NOT to let you look after my pet.\n\n\n\nYou managed to keep this poor little creature alive for {lifetime} days until it died of old age.";
        gameOverText.text = s;

        failStateGameObject.SetActive(false);
        gameOverScreen.SetActive(true);
    }

    public void RestartButtonPressed()
    {
        gameOverScreen.SetActive(false);
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
    }

    public void QuitButtonPressed()
    {
        Application.Quit();
    }
}
