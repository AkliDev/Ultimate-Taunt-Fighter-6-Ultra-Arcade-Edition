using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Idle", menuName = "FightingGame/States/Idle", order = 1)]
public class Idle : StateObject
{
    override public void OnEnter(Character refObject)
    {
        base.OnEnter(refObject);
        refObject.PhysicsComponent.Velocity = Vector2.zero;
    }

    override public void HandleInput(Character refObject)
    {
        if (refObject.InputComponent.InputStates(CardinalDirection.RIGHT) == InputState.PRESSED || refObject.InputComponent.InputStates(CardinalDirection.RIGHT) == InputState.HELD)
        {
            refObject.SwitchState(refObject.StateDictionary[CharacterStates.ForwardWalk]);
        }

        if (refObject.InputComponent.InputStates(CardinalDirection.LEFT) == InputState.PRESSED || refObject.InputComponent.InputStates(CardinalDirection.LEFT) == InputState.HELD)
        {
            refObject.SwitchState(refObject.StateDictionary[CharacterStates.BackWalk]);
        }

        if (refObject.InputComponent.InputStates(Button.BUTTON1) == InputState.PRESSED)
        {
            refObject.SwitchState(refObject.StateDictionary[CharacterStates.Attack]);
        }

        if (refObject.InputComponent.InputStates(Button.BUTTON2) == InputState.PRESSED)
        {
            refObject.SwitchState(refObject.StateDictionary[CharacterStates.Taunt]);
        }
    }

    override public void UpdateState(Character refObject)
    {
        base.UpdateState(refObject);


    }

    override public void OnExit(Character refObject)
    {

    }
}