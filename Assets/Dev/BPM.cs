using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BPM : MonoBehaviour
{
    public static BPM BPMinstance;

    public float musicBPM;

    [Header("Normal beats")]
    public static bool beatFull;
    public static int beatCountFull;
    private float beatInterval;
    private float beatTimer;

    [Header("D8 beats")] // used to devide every normal beat to 8
    public static bool beatFullD8;
    public static int beatCountFullD8;
    private float beatTimerD8;
    private float beatIntervalD8;

    private void Awake()
    {
        if(BPMinstance != null && BPMinstance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            BPMinstance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    private void Update()
    {
        BeatDetection();
    }

    void BeatDetection()
    {
        beatFull = false;
        beatInterval = 60 / musicBPM; // this is the interval of beats, if song has 60 bpm, then 60/60 is 1 -> every second there will be a beat
        beatTimer += Time.deltaTime;

        if(beatTimer >= beatInterval)
        {
            beatTimer -= beatInterval;
            beatFull = true;
            beatCountFull++;
        }

        // devided beat count (by 8)
        beatFullD8 = false;
        beatIntervalD8 = beatInterval / 8;
        beatTimerD8 += Time.deltaTime;

        if (beatTimerD8 >= beatIntervalD8)
        {
            beatTimerD8 -= beatIntervalD8;
            beatFullD8 = true;
            beatCountFullD8++;
        }
    }
}
