using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveOnBeat : MonoBehaviour
{
    public Transform target;

    public Vector3 targetPos;
    private Vector3 currentPos;

    [Header("Beat settings")]
    [Range(0, 3)]
    public int OnFullBeat;
    private int beatCountFull;
    [Range(0, 7)]
    public int[] onBeatD8;
    public bool isEveryBeat;

    void Start()
    {
        if (target == null)
        {
            target = transform;
        }
    }

    void Update()
    {
        if (!BPM.beatOn) return;

        CheckBeat();

        if (Input.GetKeyDown(KeyCode.X))
        {
            isEveryBeat = true;
        }
    }

    void Move()
    {
        LeanTween.moveLocal(gameObject, targetPos, BPM.BPMinstance.beatInterval).setEase(LeanTweenType.punch);
    }

    void CheckBeat()
    {
        if (isEveryBeat)
        {
            if (BPM.beatFull)
            {
                Move();
            }
            else
            {
                transform.localPosition = Vector3.zero;
            }
            return;
        }


        beatCountFull = BPM.beatCountFull % 4; //every sequence of 4 beats;

        for (int i = 0; i < onBeatD8.Length; i++)
        {
            if (BPM.beatFullD8 && beatCountFull == OnFullBeat && BPM.beatCountFullD8 % 8 == onBeatD8[i])
            {
                Move();
            }
        }
    }

}
