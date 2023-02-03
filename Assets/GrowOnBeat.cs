using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrowOnBeat : MonoBehaviour
{
    public Transform target;

    private float currentSize;
    public float growSize;
    public float shrinkSize;

    [Range(0.8f, 0.99f)]
    public float shrinkFactor;

    [Header("Beat settings")]
    [Range(0, 3)]
    public int OnFullBeat;
    private int beatCountFull;
    [Range(0, 7)]
    public int[] onBeatD8;
    public bool isEveryBeat;
    void Start()
    {
        if(target == null)
        {
            target = transform;
        }

        currentSize = shrinkSize;
    }

    void Update()
    {

        if (currentSize > shrinkSize)
        {
            currentSize *= shrinkFactor;
        }
        else
        {
            currentSize = shrinkSize;
        }

        CheckBeat();

        target.localScale = new Vector3(target.localScale.x, currentSize, target.localScale.z);

        if(Input.GetKeyDown(KeyCode.X))
        {
            isEveryBeat = true;
        }
    }

    void Grow()
    {
        currentSize = growSize;
    }

    void CheckBeat()
    {
        if(isEveryBeat)
        {
            if(BPM.beatFull)
            {
                Grow();
            }

            return;
        }


        beatCountFull = BPM.beatCountFull % 4; //every sequence of 4 beats;

        for (int i = 0; i < onBeatD8.Length; i++)
        {
            if(BPM.beatFullD8 && beatCountFull == OnFullBeat && BPM.beatCountFullD8 % 8 == onBeatD8[i])
            {
                Grow();
            }
        }
    }
}
