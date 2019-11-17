using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "StageBounds", menuName = "FightingGame/StageBounds", order = 1)]
public class Stage : ScriptableObject
{

    [SerializeField] float m_Bounds = 5;
    public float Bounds { get { return m_Bounds; } }
}
