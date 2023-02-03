using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

[Serializable]
public class NarrativeElement
{
    public Sprite entrySprite;
    public float fadeOutTime;
    public float fadeInTime;
}
[CreateAssetMenu(fileName = "Narrative Element", menuName = "ScriptableObjects/Create Narrative Element")]
public class NarrativeSO : ScriptableObject
{
    public NarrativeElement[] narrativeElements;
}
