using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public enum CharacterStates
{
    Idle,
    ForwardWalk,
    BackWalk,
    Attack,
    Taunt,
    KnockBack
}

[Serializable]
public class StateObjectDictionary : SerializableDictionary<CharacterStates, StateObject> { }

[RequireComponent(typeof(PhysicsComponent), typeof(InputComponent), typeof(BoxPoolComponent))]
public class Character : MonoBehaviour
{
    public delegate void IsHit(StrikeBox strikeBox);
    public event IsHit m_IsHit;

    public delegate void HasHit(StrikeBox strikeBox);
    public event HasHit m_HasHit;

    PhysicsComponent m_PhysicsComponent;
    public PhysicsComponent PhysicsComponent { get { return m_PhysicsComponent; } }

    InputComponent m_InputComponent;
    public InputComponent InputComponent { get { return m_InputComponent; } }

    BoxPoolComponent m_BoxPoolComponent;
    public BoxPoolComponent BoxPoolComponent { get { return m_BoxPoolComponent; } }

    [SerializeField] CharacterStats m_CharacterStats;
    public CharacterStats CharacterStats { get { return m_CharacterStats; } }

    protected AnimationController m_AnimationController;
    protected SpriteRenderer m_SpriteRenderer;

    [SerializeField] protected StateObject m_CurrentState;

    [SerializeField] protected int m_StateTimer;
    public int StateTimer { get { return m_StateTimer; } }
    [SerializeField]protected int m_HitStopTimer;
    protected float m_HitRumbleIntensity;
    protected float m_HitRumbleSpeed;

    [SerializeField] StateObjectDictionary m_StateDictionary;
    public StateObjectDictionary StateDictionary { get { return m_StateDictionary; } }

    [SerializeField] bool m_IsFlipped;
    public bool IsFlipped { get { return m_IsFlipped; } set { m_IsFlipped = value; } }

    protected void Awake()
    {
        m_HasHit += OnIsHit;
        m_HasHit += OnHasHit;
        m_PhysicsComponent = GetComponent<PhysicsComponent>();
        m_InputComponent = GetComponent<InputComponent>();
        m_BoxPoolComponent = GetComponent<BoxPoolComponent>();

        m_AnimationController = transform.Find("Sprite").GetComponent<AnimationController>();
        m_SpriteRenderer = transform.Find("Sprite").GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        SwitchState(m_StateDictionary[CharacterStates.Idle]);
    }

    public void UpdateCharacter()
    {
            m_SpriteRenderer.flipX = IsFlipped;
        if (m_HitStopTimer <= 0)
        {
            m_SpriteRenderer.transform.localPosition = Vector3.zero;
            m_AnimationController.IsPlaying = true;
            m_PhysicsComponent.Pause = false;
            m_CurrentState.HandleInput(this);
            m_CurrentState.UpdateState(this);
            HandleTimers();
        }
        else
        {
            m_PhysicsComponent.Pause = true;
            m_AnimationController.IsPlaying = false;
            DoHitStop();
        }    
    }

    public void PlayAnimation(SpriteAnimation clip)
    {
        m_AnimationController.PlayAnimation(clip);
    }

    public void SwitchState(StateObject newState)
    {
        if (newState == null)
            return;
        if (m_CurrentState != null)
            m_CurrentState.OnExit(this);

        m_StateTimer = -1;
        m_CurrentState = newState;
        m_CurrentState.OnEnter(this);

    }

    protected void HandleTimers()
    {
        m_StateTimer++;
    }

    public void ActivateBox(StrikeBox box)
    {
        m_BoxPoolComponent.ActivateBox(box);
    }

    public void InvokeIsHit(StrikeBox strikeBox)
    {
        OnIsHit(strikeBox);
    }

    public void InvokeHasHit(StrikeBox strikeBox)
    {
        OnHasHit(strikeBox);
    }

    void OnIsHit(StrikeBox strikeBox)
    {
        SwitchState(StateDictionary[CharacterStates.KnockBack]);
        PhysicsComponent.Velocity = strikeBox.KnockBack;
        if (!IsFlipped)
            PhysicsComponent.VelocityX = -PhysicsComponent.Velocity.x;
        m_HitStopTimer = strikeBox.HitStop.y;
        m_HitRumbleIntensity = strikeBox.HitRumbleIntesity;
        m_HitRumbleSpeed = strikeBox.HitRumbleSpeed;
    }

    void OnHasHit(StrikeBox strikeBox)
    {
        GameManager.instance.AddScore(this);
        m_HitStopTimer = strikeBox.HitStop.x;
    }

    void DoHitStop()
    {
        
        if (m_HitStopTimer > 0)
            --m_HitStopTimer;

        float rumblePosition = Mathf.Sin(Time.time * m_HitRumbleSpeed) * m_HitRumbleIntensity;
        m_SpriteRenderer.transform.localPosition = new Vector3(rumblePosition, 0, 0);

    }
}