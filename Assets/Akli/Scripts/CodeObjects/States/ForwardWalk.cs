using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ForwardWalk", menuName = "FightingGame/States/ForwardWalk", order = 1)]
public class ForwardWalk : StateObject
{
    override public void OnEnter(Character refObject)
    {
        base.OnEnter(refObject);
        refObject.PhysicsComponent.Velocity = new Vector2(refObject.CharacterStats.ForwardSpeed, 0);

        if (refObject.IsFlipped)
            refObject.PhysicsComponent.Velocity = -refObject.PhysicsComponent.Velocity;

    }

    override public void HandleInput(Character refObject)
    {
        if (refObject.InputComponent.InputStates(CardinalDirection.RIGHT) == InputState.RELEASED)
        {
            refObject.SwitchState(refObject.StateDictionary[CharacterStates.Idle]);
        }

        if (refObject.InputComponent.InputStates(CardinalDirection.LEFT) == InputState.PRESSED)
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
