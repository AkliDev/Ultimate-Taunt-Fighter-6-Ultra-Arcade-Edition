using System;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class AnimationController : MonoBehaviour
{
    public Action OnAnimationLoop;

    [SerializeField] private SpriteRenderer m_SpriteRenderer;

    [SerializeField] private SpriteAnimation m_CurrentAnimation;
    public SpriteAnimation CurrentAnimation { get { return m_CurrentAnimation; } }

    [SerializeField] private bool m_IsPlaying;
    public bool IsPlaying { get { return m_IsPlaying;  } set { m_IsPlaying = value; } }

    private int m_AnimationTimer;
    [SerializeField] private int m_CurrentFrame;

    private void Awake()
    {
        m_SpriteRenderer = GetComponent<SpriteRenderer>();
        m_IsPlaying = false;
        m_CurrentFrame = 0;
    }

    public void PlayAnimation(SpriteAnimation animation)
    {
        m_AnimationTimer = 0;
        m_CurrentFrame = 0;
        m_IsPlaying = true;
        m_CurrentAnimation = animation;
        m_SpriteRenderer.sprite = animation.Frames[0];
    }

  
    private void Update()
    {
        if (IsPlaying)
        {
            UpdateAnimationTime();
            UpdateAnimation();
        }
    }

    private void UpdateAnimationTime()
    {
        m_AnimationTimer++;
    }

    private void UpdateAnimation()
    {
        if (CurrentAnimation == null)
            return;

        if (CurrentAnimation.FrameInterval == 0)
            return;

        if (m_AnimationTimer % CurrentAnimation.FrameInterval == 0)
            m_CurrentFrame++;

        if (CurrentAnimation.LoopAnimation && m_CurrentFrame > CurrentAnimation.Frames.Length - 1)
        {
            m_CurrentFrame = 0;
        }

        if (m_CurrentFrame >= 0 && m_CurrentFrame < CurrentAnimation.Frames.Length)
            m_SpriteRenderer.sprite = CurrentAnimation.Frames[m_CurrentFrame];
    }
}