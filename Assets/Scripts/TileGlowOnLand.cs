using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileGlowOnLand : MonoBehaviour
{
    public static bool rowGlow;

    [SerializeField] bool glowOnAllRows;

    [SerializeField] GameObject[] firstLine;

    void Start()
    {
        rowGlow = glowOnAllRows;
    }

    void Update()
    {
        if (BPM.beatFull)
        {
            
        }
    }
    public static void Slam()
    {
        Debug.Log("Slam!");
    }
}
