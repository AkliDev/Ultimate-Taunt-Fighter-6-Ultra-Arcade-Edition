using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VertexBox : MonoBehaviour
{
    [SerializeField]BoxCollider2D m_Collider;
    public BoxCollider2D Collider {get { return m_Collider; } }

    private void Awake()
    {
        m_Collider = GetComponent<BoxCollider2D>();
    }

    public Vector2 GetWorldMaxOfBox()
    {
        return transform.position + (Vector3)m_Collider.offset + m_Collider.bounds.extents;
    }

    public Vector2 GetWorldMinOfBox()
    {
        return transform.position + (Vector3)m_Collider.offset - m_Collider.bounds.extents;
    }

    public Vector2 GetWorldPositionOfBox()
    {
        return new Vector2(transform.position.x, transform.position.y) + m_Collider.offset;
    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(GetWorldMinOfBox(), 0.5f);
        Gizmos.DrawSphere(GetWorldMaxOfBox(), 0.5f);
        Gizmos.DrawSphere(GetWorldPositionOfBox(), 0.5f);
    }
}
