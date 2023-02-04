using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;
    public Animator fadeAnim;

    public float afterStartDelayTime;
    public float afterDieDelayTime;

    public bool doNarrative;

    private void Start()
    {
        instance = this;
        fadeAnim.SetTrigger("Fade Into Level");
        StartCoroutine(OnStartLevelAction());
        LeanTween.init(5000);
    }

    IEnumerator OnStartLevelAction()
    {
        yield return new WaitForSeconds(afterStartDelayTime);
        //start dialogue sequence;
        // on end of dialogue sequence we start the game after X seconds
        
        if(doNarrative)
        {
            NarrativeManager.instance.StartNarrativeSequence();
        }
        else
        {
            StartCoroutine(OnEndNarrative());
        }
    }

    public IEnumerator OnEndNarrative()
    {
        yield return new WaitForSeconds(2);
        BPM.beatOn = true;
        BPM.BPMinstance.source.Play();
    }
    public void OnEndTutorial()
    {
        BPM.beatOn = true;
        BPM.BPMinstance.source.UnPause();
    }
    public void OnDie()
    {
        BPM.beatOn = false;

        fadeAnim.SetTrigger("Fade Out Level");
        StartCoroutine(OnDieAction());
    }

    IEnumerator OnDieAction()
    {
        yield return new WaitForSeconds(afterDieDelayTime);
        SceneManagerObject.instance.LoadSpecificScene(0);
    }
}
