using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorOnBeat : MonoBehaviour
{
    public Transform bodyTarget;
    public Transform hairTarget;
    public Transform leftHornTarget;
    public Transform rightHornTarget;
    private SkinnedMeshRenderer bodyMeshRenderer;
    private SkinnedMeshRenderer hairMeshRenderer;
    private SkinnedMeshRenderer hornLeftMeshRenderer;
    private SkinnedMeshRenderer hornRightMeshRenderer;
    //public Material material;
    //private Material materialInstance;
    public Color materialColor;
    public string colorProperties;

    private float colorStrength;

    [Range(0.8f, 0.99f)]
    public float fallBackFactor;

    public float colorMultiplier;

    [Header("Beat settings")]
    [Range(0, 3)]
    public int OnFullBeat;
    private int beatCountFull;
    [Range(0, 7)]
    public int[] onBeatD8;
    public bool isEveryBeat;

    Material bodyMat;
    Material HairMat;
    Material hornLeftMat;
    Material hornRighttMat;
    void Start()
    {
        if (bodyTarget != null)
        {
            bodyMeshRenderer = bodyTarget.GetComponent<SkinnedMeshRenderer>();
        }

        if (hairTarget != null)
        {
            hairMeshRenderer = hairTarget.GetComponent<SkinnedMeshRenderer>();
        }

        if (leftHornTarget != null)
        {
            hornLeftMeshRenderer = leftHornTarget.GetComponent<SkinnedMeshRenderer>();
        }

        if (rightHornTarget != null)
        {
            hornRightMeshRenderer = rightHornTarget.GetComponent<SkinnedMeshRenderer>();
        }

        colorStrength = 0;
        bodyMat = bodyMeshRenderer.materials[0];
        bodyMat.EnableKeyword("_Emmision");

        HairMat = hairMeshRenderer.materials[0];
        HairMat.EnableKeyword("_Emmision");

        hornLeftMat = hornLeftMeshRenderer.materials[0];
        hornLeftMat.EnableKeyword("_Emmision");

        hornRighttMat = hornRightMeshRenderer.materials[0];
        hornRighttMat.EnableKeyword("_Emmision");
        //materialInstance = new Material(material);
        //materialInstance.EnableKeyword("_Emmision");
        //meshRenderer.material = materialInstance;
    }

    void Update()
    {
        if(colorStrength > 1)
        {
            colorStrength *= fallBackFactor;
        }
        else
        {
            colorStrength = 1;
        }

        CheckBeat();

        //materialInstance.SetColor(colorProperties, materialColor * colorStrength * colorMultiplier);
        bodyMat.SetColor(colorProperties, materialColor * colorStrength);
        HairMat.SetColor(colorProperties, materialColor * colorStrength);
        hornLeftMat.SetColor(colorProperties, materialColor * colorStrength);
        hornRighttMat.SetColor(colorProperties, materialColor * colorStrength);

        if (Input.GetKeyDown(KeyCode.X))
        {
            isEveryBeat = true;
        }
    }

    public void Colorize()
    {
        colorStrength = 1 * colorMultiplier;
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
