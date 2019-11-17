using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public interface IState
{
    void OnEnter(Character refObject);
    void HandleInput(Character refObject);
    void UpdateState(Character refObject);
    void OnExit(Character refObject);
}

[System.Serializable]
public class StateObject : ScriptableObject, IState
{
    [SerializeField] protected int m_ExitTime = -1;
    [SerializeField] protected SpriteAnimation m_Animation;
    [SerializeField] BoxFrameData m_BoxFrameData;

    virtual public void OnEnter(Character refObject)
    {
        if (m_Animation != null)
            refObject.PlayAnimation(m_Animation);
    }

    virtual public void HandleInput(Character refObject)
    {

    }

    virtual public void UpdateState(Character refObject)
    {
        if (m_BoxFrameData != null)
        {
            var keys = from box in m_BoxFrameData.StrikeBoxDictionary where box.Value == refObject.StateTimer select box.Key; ;
            foreach (var key in keys)
            {
                StrikeBox strikeBox = (StrikeBox)key;
                refObject.ActivateBox(strikeBox);
            }
        }
    }

    virtual public void OnExit(Character refObject)
    {

    }
}
