using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BackWalk", menuName = "FightingGame/States/BackWalk", order = 1)]
public class BackWalk : StateObject
{
    override public void OnEnter(Character refObject)
    {
        base.OnEnter(refObject);
        
        refObject.PhysicsComponent.Velocity = new Vector2(refObject.CharacterStats.BackSpeed, 0);

        if(refObject.IsFlipped)
            refObject.PhysicsComponent.Velocity = -refObject.PhysicsComponent.Velocity;

    }

    override public void HandleInput(Character refObject)
    {
        if (refObject.InputComponent.InputStates(CardinalDirection.LEFT) == InputState.RELEASED)
        {
            refObject.SwitchState(refObject.StateDictionary[CharacterStates.Idle]);
        }

        if (refObject.InputComponent.InputStates(CardinalDirection.RIGHT) == InputState.PRESSED)
        {
            refObject.SwitchState(refObject.StateDictionary[CharacterStates.ForwardWalk]);
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