using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum Player
{
    First,
    Second
}

public class Inputs : MonoBehaviour
{


    //Inputs
    [SerializeField] Player _Player;
    [SerializeField] private bool _IsUsingController;

    public bool _AttackButtonUp, _AttackButtonDown, _AttackButtonHeld,
                _LeftButtonUp, _LeftButtonDown, _LeftButtonHeld,
                _RightButtonUp, _RightButtonDown, _RightButtonHeld,
                _TauntButtonUp, _TauntButtonDown, _TauntButtonHeld;

    public float _MoveAxis;

    void Update()
    {
        GetInput();
    }

    private void GetInput()
    {


        if (_Player == Player.First)
        {

            _AttackButtonUp = Input.GetKeyUp(KeyCode.G);
            _AttackButtonDown = Input.GetKeyDown(KeyCode.G);
            _AttackButtonHeld = Input.GetKey(KeyCode.G);

            _LeftButtonUp = Input.GetKeyUp(KeyCode.A);
            _LeftButtonDown = Input.GetKeyDown(KeyCode.A);
            _LeftButtonHeld = Input.GetKey(KeyCode.A);

            _RightButtonUp = Input.GetKeyUp(KeyCode.D);
            _RightButtonDown = Input.GetKeyDown(KeyCode.D);
            _RightButtonHeld = Input.GetKey(KeyCode.D);

            _TauntButtonUp = Input.GetKeyUp(KeyCode.H);
            _TauntButtonDown = Input.GetKeyDown(KeyCode.H);
            _TauntButtonHeld = Input.GetKey(KeyCode.H);
        }

        else if (_Player == Player.Second)
        {
            _AttackButtonUp = Input.GetKeyUp(KeyCode.Keypad2);
            _AttackButtonDown = Input.GetKeyDown(KeyCode.Keypad2);
            _AttackButtonHeld = Input.GetKey(KeyCode.Keypad2);

            _LeftButtonUp = Input.GetKeyUp(KeyCode.LeftArrow);
            _LeftButtonDown = Input.GetKeyDown(KeyCode.LeftArrow);
            _LeftButtonHeld = Input.GetKey(KeyCode.LeftArrow);

            _RightButtonUp = Input.GetKeyUp(KeyCode.RightArrow);
            _RightButtonDown = Input.GetKeyDown(KeyCode.RightArrow);
            _RightButtonHeld = Input.GetKey(KeyCode.RightArrow);

            _TauntButtonUp = Input.GetKeyUp(KeyCode.Keypad3);
            _TauntButtonDown = Input.GetKeyDown(KeyCode.Keypad3);
            _TauntButtonHeld = Input.GetKey(KeyCode.Keypad3);
        }


    }

    public void ResetInput()
    {
        _AttackButtonUp = false;
        _AttackButtonDown = false;
        _AttackButtonHeld = false;

        _LeftButtonUp = false;
        _LeftButtonDown = false;
        _LeftButtonHeld = false;

        _RightButtonUp = false;
        _RightButtonDown = false;
        _RightButtonHeld = false;

        _TauntButtonUp = false;
        _TauntButtonDown = false;
        _TauntButtonHeld = false;
    }
}
