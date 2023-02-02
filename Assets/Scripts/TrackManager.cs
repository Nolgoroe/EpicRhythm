using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackManager : MonoBehaviour
{
    public GameObject[] Tiles;

    private bool gridSwitch;
    void Start()
    {
        
    }

    void Update()
    {
        BeatControl();
    }
    void BeatControl()
    {
        if (BPM.beatFull)
        {
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
                    Tiles[i].SetActive(false);
                }
                if (i % 2 == 1)
                {
                    Tiles[i].SetActive(true);
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
                    Tiles[i].SetActive(true);
                }
                if (i % 2 == 1)
                {
                    Tiles[i].SetActive(false);
                }
            }
            gridSwitch = !gridSwitch;
        }
    }
    void LineControl()
    {
        for (int i = 0; i < Tiles.Length; i++)
        {
            if (i + 1 == Tiles.Length)
            {
                bool rand;
                int randomNum = Random.Range(0, 2);
                if (randomNum == 0)
                    rand = false;
                else
                    rand = true;
                Tiles[i].SetActive(rand);
            }
            else if (i == 27)
            {
                Tiles[i].SetActive(Tiles[29].activeSelf);
            }
            else if (i % 3 == 0)
            {
                Tiles[i].SetActive(Tiles[i + 3].activeSelf);
            }
        }
    }
}
