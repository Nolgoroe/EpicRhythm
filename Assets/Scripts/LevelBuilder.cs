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
}
