using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CharacterStats", menuName = "FightingGame/CharacterStats", order = 1)]
public class CharacterStats : ScriptableObject
{
    [SerializeField] float m_ForwardSpeed = 1.0f;
    public float ForwardSpeed { get { return m_ForwardSpeed; } }

    [SerializeField] float m_BackSpeed = 0.8f;
    public float BackSpeed { get { return m_BackSpeed; } }
}
