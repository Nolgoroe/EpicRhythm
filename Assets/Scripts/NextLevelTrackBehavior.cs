using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class NextLevelTrackBehavior : MonoBehaviour
{
    public static bool pianoLevel;

    public static bool stopSpawningTiles;
    public static bool stopSpawningObstacles;

    public MeshRenderer[] Tiles;
    public MeshRenderer[] slamTiles;
    public GameObject boxVolume;

    public GameObject bpm;
    public GameObject nextSong;
    private AudioSource currentSong;
    private BPM bpmScript;

    [SerializeField] Texture neon;
    [SerializeField] Texture piano;

    [SerializeField] ParticleSystem[] slamEffectMat;

    [SerializeField] int obstaclesWaitTime;
    private int obstaclesWaitIndicator;

    private int startCountingToNextLevel;
    private int slamTileWait;
    private bool volumeBoxMoved;
    private void Start()
    {
        bpmScript = bpm.GetComponent<BPM>();
        currentSong = bpm.GetComponent<AudioSource>();
    }
    private void Update()
    {
        if (!BPM.beatOn) return;

        if (BPM.beatFull)
        {
            startCountingToNextLevel++;
            if (startCountingToNextLevel == 80)
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
                if (!volumeBoxMoved) BoxVolumeMove();
                ChangeTiles();
            }
        }
    }

    void ChangeTiles()
    {
        if (slamTileWait == 39)
        {
            ChangeLevel();
        }
        slamTileWait++;
        for (int i = 0; i < Tiles.Length; i++)
        {
            
            if (i == 117 || i == 118 || i == 119)
            {
                Tiles[i].material.SetTexture("_EmissionMap", neon);
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
        {
            volumeBoxMoved = true;
            return;
        }
        else
            boxVolume.transform.Translate(0, 0, -2);
     }
    void ChangeLevel()
    {
        bpmScript.musicBPM = 114;
        currentSong.Stop();
        nextSong.SetActive(true);
        slamTiles[0].material.SetTexture("_EmissionMap", neon);
        slamTiles[1].material.SetTexture("_EmissionMap", neon);
        slamTiles[2].material.SetTexture("_EmissionMap", neon);
        slamEffectMat[0].GetComponent<ParticleSystemRenderer>().material.SetColor("_EmissionColor", Color.cyan);
        slamEffectMat[1].GetComponent<ParticleSystemRenderer>().material.SetColor("_EmissionColor", Color.cyan);
        slamEffectMat[2].GetComponent<ParticleSystemRenderer>().material.SetColor("_EmissionColor", Color.cyan);
    }
}
