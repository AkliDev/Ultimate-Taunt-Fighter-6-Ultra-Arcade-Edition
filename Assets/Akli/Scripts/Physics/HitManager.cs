using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitManager : MonoBehaviour
{
    [SerializeField] private List<Character> m_HitCharacters;
    Character m_Caracter;
    StrikeBoxComponent m_StrikeBoxComponent;
    BoxCollider2D m_BoxCollider;
    [SerializeField] int m_ActiveTimer;

    private void Awake()
    {
        m_BoxCollider = GetComponent<BoxCollider2D>();
        m_HitCharacters = new List<Character>();
        m_StrikeBoxComponent = GetComponent<StrikeBoxComponent>();
        m_Caracter = GetComponentInParent<Character>();
    }

    public void Activate(StrikeBox strikeBox)
    {
        m_StrikeBoxComponent.m_StrikeBox = strikeBox;
        m_BoxCollider.offset = strikeBox.Offset;

        if (m_Caracter.IsFlipped)
            m_BoxCollider.offset = new Vector2(-m_BoxCollider.offset.x, m_BoxCollider.offset.y);

        m_BoxCollider.size = strikeBox.Size;

        m_BoxCollider.enabled = true;
    }

    void ClearHitCharacters()
    {
        m_HitCharacters.Clear();
    }

    private void Update()
    {
        if (m_BoxCollider.enabled == true)
        {
            if (m_ActiveTimer < m_StrikeBoxComponent.m_StrikeBox.ActiveTime)
            {
                m_ActiveTimer++;
            }
            else
            {
                m_ActiveTimer = 0;
                ClearHitCharacters();
                m_BoxCollider.enabled = false;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Character otherCharacter = collision.GetComponent<Character>();
        if (otherCharacter != m_Caracter)
            if (!m_HitCharacters.Contains(otherCharacter))
            {
                m_HitCharacters.Add(otherCharacter);
                otherCharacter.InvokeIsHit(m_StrikeBoxComponent.m_StrikeBox);
                m_Caracter.InvokeHasHit(m_StrikeBoxComponent.m_StrikeBox);
            }
        print("hit");
    }
}
