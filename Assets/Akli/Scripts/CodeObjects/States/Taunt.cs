using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Taunt", menuName = "FightingGame/States/Taunt", order = 1)]
public class Taunt : StateObject
{
    override public void OnEnter(Character refObject) 
    {
        base.OnEnter(refObject);
        refObject.PhysicsComponent.Velocity = Vector2.zero;
    }

    override public void HandleInput(Character refObject)
    {

    }

    override public void UpdateState(Character refObject)
    {
        base.UpdateState(refObject);
        if (refObject.StateTimer >= m_ExitTime)
            refObject.SwitchState(refObject.StateDictionary[CharacterStates.Idle]);
    }

    override public void OnExit(Character refObject)
    {

    }
}