using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileGlowOnLand : MonoBehaviour
{

    [SerializeField] bool glowOnAllRows;
    [SerializeField] MeshRenderer[] firstLine;
    [SerializeField] ParticleSystem[] slamParticleEffect;

    private Color defaultColor;
    void Start()
    {
        defaultColor = firstLine[0].material.color;
    }
    private void Update()
    {
        if (BPM.beatFull)
        {
            firstLine[0].enabled = true;
            firstLine[1].enabled = true;
            firstLine[2].enabled = true;
        }
    }

    public void Slam()
    {
        
        switch (PlayerController.currentPos)
        {
            case 0:
                firstLine[0].enabled = false;
                slamParticleEffect[0].Play();
                break;
            case 1:
                slamParticleEffect[1].Play();
               firstLine[1].enabled = false;
                break;
            case 2:
                slamParticleEffect[2].Play();
               firstLine[2].enabled = false;
                break;
        }
        
    }
}
