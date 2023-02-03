using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileGlowOnLand : MonoBehaviour
{

    [SerializeField] bool glowOnAllRows;
    [SerializeField] MeshRenderer[] firstLine;
    [SerializeField] ParticleSystem[] slamParticleEffect;
    [SerializeField] ParticleSystem[] crackEffect;
    [SerializeField] GameObject[] crackObject;

    private Color defaultColor;
    void Start()
    {
        defaultColor = firstLine[0].material.color;
    }
    private void Update()
    {
        if (BPM.beatFull)
        {
            firstLine[0].material.color = defaultColor;
            firstLine[1].material.color = defaultColor;
            firstLine[2].material.color = defaultColor;
        }
    }

    public void Slam()
    {
        
        switch (PlayerController.currentPos)
        {
            case 0:
               firstLine[0].material.color = Color.green;
                slamParticleEffect[0].Play();
                break;
            case 1:
                slamParticleEffect[1].Play();
               firstLine[1].material.color = Color.green;
                break;
            case 2:
                slamParticleEffect[2].Play();
               firstLine[2].material.color = Color.green;
                break;
        }
        
    }
}
