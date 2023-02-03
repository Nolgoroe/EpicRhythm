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

    private void Start()
    {
        instance = this;
        fadeAnim.SetTrigger("Fade Into Level");
        StartCoroutine(OnStartLevelAction());
    }

    IEnumerator OnStartLevelAction()
    {
        yield return new WaitForSeconds(afterStartDelayTime);
        BPM.beatOn = true;
        BPM.BPMinstance.source.Play();
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
