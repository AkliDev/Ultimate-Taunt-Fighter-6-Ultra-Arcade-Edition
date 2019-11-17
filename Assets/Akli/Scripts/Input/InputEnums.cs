using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum CardinalDirection
{ 
    UP,
    DOWN,
    LEFT,
    RIGHT
};

public enum DirectionNotation 
{
    DOWN_BACK = 1,
    DOWN,
    DOWN_FORWARD,
    BACK,
    NEUTRAL,
    FORWARD,
    UP_BACK,
    UP,
    UP_FORWARD,
};

public enum Button 
{
    BUTTON1,
    BUTTON2,
    BUTTON3,
    BUTTON4,
    BUTTON5,
    BUTTON6,
};

public enum InputState
{
    RELEASED = -1,
    NOT_PRESSED = 0,
    PRESSED = 1,
    HELD = 2
};

