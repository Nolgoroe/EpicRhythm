using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextLevelTrackBehavior : MonoBehaviour
{
    public static bool stopSpawningTiles;
    public static bool stopSpawningObstacles;

    public MeshRenderer[] Tiles;
    public GameObject boxVolume;

    [SerializeField] Texture neon;
    [SerializeField] Texture piano;

    [SerializeField] int obstaclesWaitTime;
    private int obstaclesWaitIndicator;
    private void Update()
    {
        if (BPM.beatFull)
        {
            if (stopSpawningObstacles)
            {
                obstaclesWaitIndicator++;
                if (obstaclesWaitIndicator == obstaclesWaitTime)
                    stopSpawningTiles = true;
            }
            if (stopSpawningTiles)
            {
                boxVolume.transform.Translate(0,0,-2);
                ChangeTiles();
            }
        }
    }

    void ChangeTiles()
    {
        for (int i = 0; i < Tiles.Length; i++)
        {
            
            if (i == 27 || i == 28 || i == 29)
            {
                Tiles[i].material.SetTexture("_EmissionMap", piano);
            }
            else if (i % 3 == 0)
            {
                Tiles[i].material.SetTexture("_EmissionMap", Tiles[i+3].material.GetTexture("_EmissionMap"));
            }
            else if (i % 3 == 1)
            {
                Tiles[i].material.SetTexture("_EmissionMap", Tiles[i + 3].material.GetTexture("_EmissionMap"));
            }
            else if (i % 3 == 2)
            {
                Tiles[i].material.SetTexture("_EmissionMap", Tiles[i + 3].material.GetTexture("_EmissionMap"));
            }
        }
    }
}
