using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

[Serializable]
public class StrikeBox
{
    public Vector2 Offset;
    public Vector2 Size;
    public AudioClip Audio;
    public int ActiveTime;
    public Vector2Int HitStop;
    public Vector2 KnockBack;
    public float HitRumbleIntesity;
    public float HitRumbleSpeed;
}

[Serializable]
public class StrikeBoxDictionary : SerializableDictionary<StrikeBox, int> { }

[CreateAssetMenu(fileName = "BoxFrameData", menuName = "FightingGame/BoxFrameData", order = 1)]
public class BoxFrameData : ScriptableObject
{
    [SerializeField] StrikeBoxDictionary m_StrikeBoxDictionary;
    public StrikeBoxDictionary StrikeBoxDictionary { get { return m_StrikeBoxDictionary; } }
}
