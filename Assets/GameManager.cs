using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public Animator fadeAnim;
    public float fadeDelayTime;

    public GameObject mainMenuPanel, creditsPanel;
    void Start()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
    }

    public void StartGame()
    {
        fadeAnim.SetTrigger("Fade Now MainMenu");
        StartCoroutine(OnEndFadeIn());
    }

    IEnumerator OnEndFadeIn()
    {
        yield return new WaitForSeconds(fadeDelayTime);
        SceneManagerObject.instance.LoadSpecificScene(1);
    }

    public void QuitGame()
    {
        Debug.Log("Exit game");
        Application.Quit();
    }

    public void GoToCredits()
    {
        mainMenuPanel.SetActive(false);
        creditsPanel.SetActive(true);
    }
    public void GoBackToMainMenu()
    {
        creditsPanel.SetActive(false);
        mainMenuPanel.SetActive(true);
    }
}
