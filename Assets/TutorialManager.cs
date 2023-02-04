using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialManager : MonoBehaviour
{
    public static TutorialManager instance;

    public NarrativeSO tutorialSO;
    public Button nextElementButton;
    public Image imageObject;

    bool alreadyHappned;

    public Canvas tutorialCanvas;

    public int tutorialIndex;
    private void Awake()
    {
        instance = this;
    }

    private void Update()
    {
        if (!BPM.beatOn) return;
        Debug.Log(BPM.beatCountFull);

        if(BPM.beatCountFull == 10 && !alreadyHappned)
        {
            alreadyHappned = true;
            StartCoroutine(FadeInImage());
        }

        if(BPM.beatCountFull != 10 && BPM.beatFull)
        {
            alreadyHappned = false;
        }
    }
    public void StopBPM()
    {
        BPM.beatOn = false;
        BPM.BPMinstance.source.Pause();
    }

    public IEnumerator FadeInImage()
    {
        StopBPM();
        tutorialCanvas.gameObject.SetActive(true);

        nextElementButton.interactable = false;
        float time = tutorialSO.narrativeElements[tutorialIndex].fadeInTime;


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
        float time = tutorialSO.narrativeElements[tutorialIndex].fadeOutTime;

        LeanTween.value(imageObject.gameObject, 1, 0, time).setOnUpdate((float val) =>
        {
            Color color = new Color(imageObject.color.r, imageObject.color.g, imageObject.color.b, val);
            imageObject.color = color;

        });

        yield return new WaitForSeconds(time);
        tutorialIndex++;

        tutorialCanvas.gameObject.SetActive(false);

        LevelManager.instance.OnEndTutorial();
    }

    public void AdvanceDialogue()
    {
        nextElementButton.interactable = false;

        StartCoroutine(FadeOutImage());
    }
}
