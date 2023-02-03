using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleTrackManager : MonoBehaviour
{
    public MeshRenderer[] meshObstacle;
    public BoxCollider[] colliderObstacle;

    [SerializeField] int instance;
    [SerializeField] LevelBuilder level;

    private int lineIndicator;
    void Start()
    {
        
    }

    void Update()
    {
        if (BPM.beatFull)
        {
            LineControl();
            if (PlayerController.inAir) DisableColliderFirstLine();
        }
    }
    bool[] ReturnNextLine()
    {
        for (int i = 0; i < level.levelPreset.Length; i++)
        {
            if (i == lineIndicator)
            {
                lineIndicator++;
                return level.levelPreset[i].line;
            }
        }
        lineIndicator = 0;
        return level.levelPreset[0].line;
    }
    void LineControl()
    {
        for (int i = 0; i < meshObstacle.Length; i++)
        {
            if (i == 27 || i == 28 || i == 29)
            {
                bool[] line = ReturnNextLine();
                switch (instance)
                {
                    case 1:
                        meshObstacle[27].enabled = line[0];
                        colliderObstacle[27].enabled = line[0];
                        meshObstacle[28].enabled = line[1];
                        colliderObstacle[28].enabled = line[1];
                        meshObstacle[29].enabled = line[2];
                        colliderObstacle[29].enabled = line[2];
                        break;
                    case 2:
                        meshObstacle[27].enabled = line[3];
                        colliderObstacle[27].enabled = line[3];
                        meshObstacle[28].enabled = line[4];
                        colliderObstacle[28].enabled = line[4];
                        meshObstacle[29].enabled = line[5];
                        colliderObstacle[29].enabled = line[5];
                        break;
                }
            }
            else if (i % 3 == 0)
            {
                meshObstacle[i].enabled = meshObstacle[i + 3].enabled;
                colliderObstacle[i].enabled = colliderObstacle[i + 3].enabled;
            }
            else if (i % 3 == 1)
            {
                meshObstacle[i].enabled = meshObstacle[i + 3].enabled;
                colliderObstacle[i].enabled = colliderObstacle[i + 3].enabled;
            }
            else if (i % 3 == 2)
            {
                meshObstacle[i].enabled = meshObstacle[i + 3].enabled;
                colliderObstacle[i].enabled = colliderObstacle[i + 3].enabled;
            }
        }
    }
    void DisableColliderFirstLine()
    {
        colliderObstacle[0].enabled = false;
        colliderObstacle[1].enabled = false;
        colliderObstacle[2].enabled = false;
    }
}
