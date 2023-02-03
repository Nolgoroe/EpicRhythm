using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public Animator fadeAnim;
    public float fadeDelayTime;
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
}
