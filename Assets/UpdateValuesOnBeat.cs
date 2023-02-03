using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class UpdateValuesOnBeat : MonoBehaviour
{
    public Volume processVolume;
    public LensDistortion distortion;

    private float currentDistortion;
    public float growDistortion;
    public float shrinkDistortion;

    [Range(0.8f, 0.99f)]
    public float distortionFactor;


    [Header("Beat settings")]
    [Range(0, 3)]
    public int OnFullBeat;
    private int beatCountFull;
    [Range(0, 7)]
    public int[] onBeatD8;
    public bool isEveryBeat;
    void Start()
    {
        if(processVolume == null)
        {
            processVolume = GetComponent<Volume>();
        }

        processVolume.profile.TryGet<LensDistortion>(out distortion);

    }

    void Update()
    {

        if (currentDistortion > shrinkDistortion)
        {
            currentDistortion *= distortionFactor;
        }
        else
        {
            currentDistortion = shrinkDistortion;
        }

        CheckBeat();


        distortion.intensity.value = currentDistortion;
    }

    void Distort()
    {
        currentDistortion = growDistortion;
    }

    void CheckBeat()
    {
        if (isEveryBeat)
        {
            if (BPM.beatFull)
            {
                Distort();
            }

            return;
        }

        beatCountFull = BPM.beatCountFull % 4; //every sequence of 4 beats;

        for (int i = 0; i < onBeatD8.Length; i++)
        {
            if (BPM.beatFullD8 && beatCountFull == OnFullBeat && BPM.beatCountFullD8 % 8 == onBeatD8[i])
            {
                Distort();
            }
        }

    }
}
