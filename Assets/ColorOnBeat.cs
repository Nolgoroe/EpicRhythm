using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorOnBeat : MonoBehaviour
{
    public Transform target;
    private MeshRenderer meshRenderer;
    public Material material;
    private Material materialInstance;
    public Color materialColor;
    public string colorProperties;

    private float colorStrength;

    [Range(0.8f, 0.99f)]
    public float fallBackFactor;

    public float colorMultiplier;

    [Range(0, 3)]
    public int OnFullBeat;
    private int beatCountFull;

    [Range(0, 7)]
    public int[] onBeatD8;

    public bool isEveryBeat;

    void Start()
    {
        if (target != null)
        {
            meshRenderer = target.GetComponent<MeshRenderer>();
        }
        else
        {
            meshRenderer = GetComponent<MeshRenderer>();
        }

        colorStrength = 0;
        materialInstance = new Material(material);
        materialInstance.EnableKeyword("_EMISSION");
        meshRenderer.material = materialInstance;
    }

    void Update()
    {
        if(colorStrength > 0)
        {
            colorStrength *= fallBackFactor;
        }
        else
        {
            colorStrength = 0;
        }

        CheckBeat();

        materialInstance.SetColor(colorProperties, materialColor * colorStrength * colorMultiplier);

        if (Input.GetKeyDown(KeyCode.X))
        {
            isEveryBeat = true;
        }
    }

    void Colorize()
    {
        colorStrength = 1;
    }

    void CheckBeat()
    {
        if (isEveryBeat)
        {
            if (BPM.beatFull)
            {
                Colorize();
            }

            return;
        }

        beatCountFull = BPM.beatCountFull % 4; //every sequence of 4 beats;

        for (int i = 0; i < onBeatD8.Length; i++)
        {
            if (BPM.beatFullD8 && beatCountFull == OnFullBeat && BPM.beatCountFullD8 % 8 == onBeatD8[i])
            {
                Colorize();
            }
        }

    }
}
