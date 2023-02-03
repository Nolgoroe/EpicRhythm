using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class LinePreset
{
    public bool[] line;
}
public class LevelBuilder : MonoBehaviour
{
    public LinePreset[] levelPreset;

    private void Start()
    {

    }
   
    void SetLine(bool[] line, bool b1, bool b2, bool b3, bool b4, bool b5, bool b6)
    {
        line[0] = b1;
        line[1] = b2;
        line[2] = b3;
        line[3] = b4;
        line[4] = b5;
        line[5] = b6;
    }
}
