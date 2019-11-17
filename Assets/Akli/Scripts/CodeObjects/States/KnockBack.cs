using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "KnockBack", menuName = "FightingGame/States/KnockBack", order = 1)]
public class KnockBack : StateObject
{
    override public void OnEnter(Character refObject) 
    {
        base.OnEnter(refObject);
    }

    override public void HandleInput(Character refObject)
    {

    }

    override public void UpdateState(Character refObject)
    {
        base.UpdateState(refObject);
    }

    override public void OnExit(Character refObject)
    {

    }
}