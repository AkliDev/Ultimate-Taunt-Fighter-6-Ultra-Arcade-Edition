using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(VertexBox))]
public class PhysicsComponent : MonoBehaviour
{
    [SerializeField] private Vector2 m_Velocity;
    public Vector2 Velocity { get { return m_Velocity;  } set { m_Velocity = value; } }
    public float VelocityX {  set { m_Velocity.x = value; } }
    public float VelocityY { set { m_Velocity.y = value; } }

    [SerializeField]  private float m_GravityScale;

    BoxCollider2D m_Collider;
    public BoxCollider2D Collider { get { return m_Collider; } }
    private VertexBox m_VertexBox;
    public VertexBox PushBox { get { return m_VertexBox; } }
    public bool Pause { get; set; }

    private void Awake()
    {
        m_VertexBox = GetComponent<VertexBox>();
        m_Collider = GetComponent<BoxCollider2D>();
    }

    private void Start()
    {
        PhysicsWorld.instance.AddPushBox(this);
    }
}
