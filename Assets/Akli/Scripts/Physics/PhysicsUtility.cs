using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct BoxOverlap
{
    public float x;
    public float y;
    public Vector2 mtv;

    public BoxOverlap(float xOverlap, float yOverlap, Vector2 MTV)
    {
        x = xOverlap;
        y = yOverlap;
        mtv = MTV;
    }
}

public class PhysicsUtility : MonoBehaviour
{
    static public BoxOverlap GetOverlapOfBox(VertexBox a, VertexBox b)
    {
        float xOverlap;
        float yOverlap;
        Vector2 mtv;

        xOverlap = a.Collider.bounds.extents.x + b.Collider.bounds.extents.x - Mathf.Abs(a.transform.position.x - b.transform.position.x);
        yOverlap = a.Collider.bounds.extents.y + b.Collider.bounds.extents.y - Mathf.Abs(a.transform.position.y - b.transform.position.y);

        if (a.transform.position.x < b.transform.position.x)
            xOverlap = -xOverlap;

        if (a.transform.position.y < b.transform.position.y)
            yOverlap = -yOverlap;

        if (Mathf.Abs(xOverlap) < Mathf.Abs(yOverlap))
            mtv = new Vector2(xOverlap, 0);
        else
            mtv = new Vector2(0, yOverlap );

        return new BoxOverlap(xOverlap, yOverlap, mtv);
    }

    static public bool AreBoxesIntersecting(VertexBox a, VertexBox b)
    {
        return (Mathf.Abs(a.GetWorldPositionOfBox().x - b.GetWorldPositionOfBox().x) < a.Collider.bounds.extents.x + b.Collider.bounds.extents.x &&
                Mathf.Abs(a.GetWorldPositionOfBox().y - b.GetWorldPositionOfBox().y) < a.Collider.bounds.extents.y + b.Collider.bounds.extents.y);
    }
}



