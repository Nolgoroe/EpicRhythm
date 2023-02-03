using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleTrackManager : MonoBehaviour
{
    public MeshRenderer[] meshObstacle;
    public BoxCollider[] colliderObstacle;
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
    void LineControl()
    {
        for (int i = 0; i < meshObstacle.Length; i++)
        {
            if (i == 27 || i == 28 || i == 29)
            {
                bool rand;
                int randomNum = Random.Range(0, 101);
                if (randomNum < 90)
                    rand = false;
                else
                    rand = true;
                meshObstacle[i].enabled = rand;
                colliderObstacle[i].enabled = rand;
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
