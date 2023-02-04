using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NarrativeManager : MonoBehaviour
{
    public static NarrativeManager instance;

    public Image imageObject;
    public NarrativeSO currentNarrativeSO;

    public int currentIndex;
    int maxIndex;

    public Button nextElementButton;


    public Canvas narrativeCanvas, gameCanvas;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        //gameCanvas.gameObject.SetActive(false);
        //narrativeCanvas.gameObject.SetActive(true);
    }
    public void StartNarrativeSequence()
    {
        currentIndex = 0;
        StartCoroutine(FadeInImage());
        maxIndex = currentNarrativeSO.narrativeElements.Length;
    }
    public IEnumerator FadeInImage()
    {
        nextElementButton.interactable = false;

        imageObject.sprite = currentNarrativeSO.narrativeElements[currentIndex].entrySprite;
        float time = currentNarrativeSO.narrativeElements[currentIndex].fadeInTime;


        LeanTween.value(imageObject.gameObject, 0, 1, time).setOnUpdate((float val) =>
        {
            Color color = new Color(imageObject.color.r, imageObject.color.g, imageObject.color.b, val);
            imageObject.color = color;

        });

        yield return new WaitForSeconds(time);

        nextElementButton.interactable = true;
    }
    public IEnumerator FadeOutImage()
    {
        float time = currentNarrativeSO.narrativeElements[currentIndex].fadeOutTime;

        LeanTween.value(imageObject.gameObject, 1, 0, time).setOnUpdate((float val) =>
        {
            Color color = new Color(imageObject.color.r, imageObject.color.g, imageObject.color.b, val);
            imageObject.color = color;

        });

        yield return new WaitForSeconds(time);

        if (currentIndex + 1 == maxIndex)
        {
            narrativeCanvas.gameObject.SetActive(false);
            gameCanvas.gameObject.SetActive(true);
            StartCoroutine(LevelManager.instance.OnEndNarrative());
        }
        else
        {
            yield return new WaitForSeconds(1);
            currentIndex++;
            StartCoroutine(FadeInImage());
        }
    }

    public void AdvanceDialogue()
    {
        nextElementButton.interactable = false;

        StartCoroutine(FadeOutImage());
    }
}
