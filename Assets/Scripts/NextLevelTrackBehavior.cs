using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.VFX;
public class NextLevelTrackBehavior : MonoBehaviour
{

    public static bool pianoLevel;

    public static bool stopSpawningTiles;
    public static bool stopSpawningObstacles;

    public MeshRenderer[] Tiles;
    public MeshRenderer[] slamTiles;
    public GameObject boxVolume;
    public GameObject portalHolder;
    public GameObject portal;

    public MeshRenderer[] obstacles1;
    public MeshRenderer[] obstacles2;


    public GameObject bpm;
    public GameObject nextSongGameObject;
    private AudioSource currentSong;
    private BPM bpmScript;
    private AudioSource nextSong;
    [SerializeField] float fadeMusicTime;

    [SerializeField] Texture neon;
    [SerializeField] Texture piano;

    [SerializeField] ParticleSystem[] slamEffectMat;

    [SerializeField] int obstaclesWaitTime;
    private int obstaclesWaitIndicator;

    private int startCountingToNextLevel;
    private int slamTileWait;
    private bool volumeBoxMoved;
    private bool portalBoxMoved;
    private bool portalDisabled;
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
                if (!portalDisabled)
                {
                    portal.SetActive(true);
                    portalDisabled = true;
                }

                if (!volumeBoxMoved) BoxVolumeMove();
                if (!portalBoxMoved) PortalMove();
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
    void ChangeObstacles(MeshRenderer[] obstacles)
    {
        for (int i = 0; i < obstacles.Length; i++)
        {
            obstacles[i].material.SetTexture("_EmissionMap", neon);
            obstacles[i].material.SetColor("_EmissionColor", Color.red);
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
    void PortalMove()
    {
        if (portalHolder.transform.position.z == 24)
        {
            portal.GetComponent<VisualEffect>().Play();
            portalHolder.transform.Translate(0, 0, -2);
        }
        if (portalHolder.transform.position.z <= 0)
        {
            portal.SetActive(false);
            portalBoxMoved = true;
            return;
        }
        else
            portalHolder.transform.Translate(0, 0, -2);
    }
    void ChangeLevel()
    {
        ChangeObstacles(obstacles1);
        ChangeObstacles(obstacles2);
        stopSpawningObstacles = false;
        bpmScript.musicBPM = 120;
        fadeIn();
        fadeOut();
        slamTiles[0].material.SetTexture("_EmissionMap", neon);
        slamTiles[1].material.SetTexture("_EmissionMap", neon);
        slamTiles[2].material.SetTexture("_EmissionMap", neon);
        slamEffectMat[0].GetComponent<ParticleSystemRenderer>().material.SetColor("_EmissionColor", Color.cyan);
        slamEffectMat[1].GetComponent<ParticleSystemRenderer>().material.SetColor("_EmissionColor", Color.cyan);
        slamEffectMat[2].GetComponent<ParticleSystemRenderer>().material.SetColor("_EmissionColor", Color.cyan);
    }
    void fadeOut()
    {
        LeanTween.value(bpm, 1, 0, fadeMusicTime).setOnUpdate((float val) =>
        {
            currentSong.volume = val;
        });
    }
    void fadeIn()
    {
        nextSongGameObject.SetActive(true);
        LeanTween.value(bpm, 0, 1, fadeMusicTime).setOnUpdate((float val) =>
        {
            currentSong.volume = val;
        });
    }
}
