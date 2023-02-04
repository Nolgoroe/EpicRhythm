using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleTrackManager : MonoBehaviour
{
    public MeshRenderer[] meshObstacle;
    public BoxCollider[] colliderObstacle;

    [SerializeField] int instance;
    [SerializeField] LevelBuilder level;

    public int lineIndicator;

    private bool emptyLine;
    void Update()
    {
        if (!BPM.beatOn) return;

        if (BPM.beatFull)
        {
            LineControl();
            if (PlayerController.inAir) DisableColliderFirstLine();
        }
    }
    bool[] ReturnNextLine()
    {
        if (lineIndicator == level.levelPreset.Length) lineIndicator = 40;
        bool[] line = level.levelPreset[0].line;
        for (int i = 0; i < level.levelPreset.Length; i++)
        {
            if (i == lineIndicator)
            {
                line = level.levelPreset[i].line;
            }
        }
        lineIndicator++;
        return line;
    }
    bool[] ReturnEmptyLine()
    {
        bool[] line = new bool[6];
        for (int i = 0; i < 6; i++)
        {
            line[i] = false;
        }
        return line;
    }
    void LineControl()
    {
        bool[] line;
        if (!emptyLine)
        {
            line = ReturnNextLine();
            emptyLine = true;
        }
        else
        {
            line = ReturnEmptyLine();
            emptyLine = false;
        }
        for (int i = 0; i < meshObstacle.Length; i++)
        {
            if (i == 117 || i == 118 || i == 119)
            {
                switch (instance)
                {
                    case 1:
                        meshObstacle[117].enabled = line[0];
                        colliderObstacle[117].enabled = line[0];
                        meshObstacle[118].enabled = line[1];
                        colliderObstacle[118].enabled = line[1];
                        meshObstacle[119].enabled = line[2];
                        colliderObstacle[119].enabled = line[2];
                        break;
                    case 2:
                        meshObstacle[117].enabled = line[3];
                        colliderObstacle[117].enabled = line[3];
                        meshObstacle[118].enabled = line[4];
                        colliderObstacle[118].enabled = line[4];
                        meshObstacle[119].enabled = line[5];
                        colliderObstacle[119].enabled = line[5];
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
