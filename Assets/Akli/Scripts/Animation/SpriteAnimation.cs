using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SpriteAnimation")]
public class SpriteAnimation : ScriptableObject
{
    public int FrameInterval = 5;
    public bool LoopAnimation;
    [Space(5)]
    public Sprite[] Frames;
}
