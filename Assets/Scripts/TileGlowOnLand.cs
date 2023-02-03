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
   
    public void Slam()
    {
        if (rowGlow)
        {

        }
        else
        {
            switch (PlayerController.currentPos)
            {
                case 0:
                    firstLine[0].GetComponent<MeshRenderer>().material.color = Color.cyan;
                    break;
                case 1:
                    break;
                case 2:
                    break;
            }
        }
        
    }
}
