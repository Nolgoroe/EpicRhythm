using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileBehavior : MonoBehaviour
{


    private void OnEnable()
    {
    }
    void Update()
    {
        if (BPM.beatFull)
        {
            ActionPerBeatUpdate();
        }
    }
   
    void ActionPerBeatUpdate()
    {
        LeanTween.move(gameObject, new Vector3(transform.position.x, transform.position.y, transform.position.z + 0.5f), BPM.BPMinstance.beatInterval).setEase(LeanTweenType.punch);
    }
}
