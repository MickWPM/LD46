using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Manager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        EventsManager.instance.CharacterDeathEvent += OnCharacterDied;
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
