using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackManager : MonoBehaviour
{
    public MeshRenderer[] Tiles;

    private bool gridSwitch;
    void Start()
    {
        //GridSwitch();
    }

    void Update()
    {
        //BeatControl();
    }
    void BeatControl()
    {
        if (BPM.beatFull)
        {
            if (BPM.beatCountFull <= 6)
            {
                GridSwitch();
            }
            else
                LineControl();
        }
    }
    void GridSwitch()
    {
        if (gridSwitch)
        {
            for (int i = 0; i < Tiles.Length; i++)
            {
                if (i % 2 == 0)
                {
                    Tiles[i].enabled = false;
                }
                if (i % 2 == 1)
                {
                    Tiles[i].enabled = true;
                }
            }
            gridSwitch = !gridSwitch;
        }
        else
        {
            for (int i = 0; i < Tiles.Length; i++)
            {
                if (i % 2 == 0)
                {
                    Tiles[i].enabled = true;
                }
                if (i % 2 == 1)
                {
                    Tiles[i].enabled = false;
                }
            }
            gridSwitch = !gridSwitch;
        }
    }
    
    void LineControl()
    {
        for (int i = 0; i < Tiles.Length; i++)
        {
            if (i == 27 || i == 28 || i == 29)
            {
                bool rand;
                int randomNum = Random.Range(0, 2);
                if (randomNum == 0)
                    rand = false;
                else
                    rand = true;
                Tiles[i].enabled = rand;
            }
            else if (i % 3 == 0)
            {
                Tiles[i].enabled = Tiles[i + 3].enabled;
            }
            else if (i % 3 == 1)
            {
                Tiles[i].enabled = Tiles[i + 3].enabled;
            }
            else if (i % 3 == 2)
            {
                Tiles[i].enabled = Tiles[i + 3].enabled;
            }
        }
    }
}
