using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextLevelTrackBehavior : MonoBehaviour
{
    public static bool pianoLevel;

    public static bool stopSpawningTiles;
    public static bool stopSpawningObstacles;

    public MeshRenderer[] Tiles;
    public MeshRenderer[] slamTiles;
    public GameObject boxVolume;

    [SerializeField] Texture neon;
    [SerializeField] Texture piano;

    [SerializeField] Material slamEffectMat;

    [SerializeField] int obstaclesWaitTime;
    private int obstaclesWaitIndicator;

    private int startCountingToNextLevel;
    private int slamTileWait;
    private void Update()
    {
        if (BPM.beatFull)
        {
            startCountingToNextLevel++;
            if (startCountingToNextLevel == 60)
            {
                stopSpawningObstacles = true;
            }
            if (stopSpawningObstacles)
            {
                obstaclesWaitIndicator++;
                if (obstaclesWaitIndicator == obstaclesWaitTime)
                    stopSpawningTiles = true;
            }
            if (stopSpawningTiles)
            {
                BoxVolumeMove();
                ChangeTiles();
            }
        }
    }

    void ChangeTiles()
    {
        if (slamTileWait == 9)
        {
            slamTiles[0].material.SetTexture("_EmissionMap", piano);
            slamTiles[1].material.SetTexture("_EmissionMap", piano);
            slamTiles[2].material.SetTexture("_EmissionMap", piano);
        }
        slamTileWait++;
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
    void BoxVolumeMove()
    {
        if (boxVolume.transform.position.z <= 0)
            return;
        else
            boxVolume.transform.Translate(0, 0, -2);
;    }
}
