using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsWorld : MonoBehaviour
{
    static public PhysicsWorld instance;
    List<PhysicsComponent> m_PhysicsEntities;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this);

        m_PhysicsEntities = new List<PhysicsComponent>();
    }

    public void AddPushBox(PhysicsComponent physicsEntities)
    {
        if (m_PhysicsEntities.Contains(physicsEntities))
            return;

        m_PhysicsEntities.Add(physicsEntities);
    }

    public void UpdateWorld()
    {
        ApplyVelocity();        
        BoundsCollision();
        CollisionForPushBox();
    }

    void ApplyVelocity()
    {
        foreach (PhysicsComponent physicsEntity in m_PhysicsEntities)
        {
            if(!physicsEntity.Pause)
            physicsEntity.transform.Translate(physicsEntity.Velocity);
        }
    }

    void CollisionForPushBox()
    {
        foreach (PhysicsComponent physicsEntity in m_PhysicsEntities)
        {
            foreach (PhysicsComponent other in m_PhysicsEntities)
            {
                if (physicsEntity == other)
                    continue;

                BoxOverlap overlap = PhysicsUtility.GetOverlapOfBox(physicsEntity.PushBox, other.PushBox);

                if (PhysicsUtility.AreBoxesIntersecting(physicsEntity.PushBox, other.PushBox))
                {
                    physicsEntity.transform.Translate(new Vector3(overlap.x, 0) * 0.5f);
                    other.transform.Translate(new Vector3(overlap.x, 0) * -0.5f);
                }



                return;
            }
        }
    }

    void BoundsCollision()
    {
        foreach (PhysicsComponent physicsEntity in m_PhysicsEntities)
        {
            if (physicsEntity.transform.position.y < 0)
                physicsEntity.transform.position = new Vector3(physicsEntity.transform.position.x, 0);

            if (physicsEntity.transform.position.x - physicsEntity.Collider.bounds.extents.x < -GameManager.instance.Stage.Bounds)
                physicsEntity.transform.position = new Vector3(-GameManager.instance.Stage.Bounds + physicsEntity.Collider.bounds.extents.x, physicsEntity.transform.position.y);

            if (physicsEntity.transform.position.x + physicsEntity.Collider.bounds.extents.x > GameManager.instance.Stage.Bounds)
                physicsEntity.transform.position = new Vector3(GameManager.instance.Stage.Bounds - physicsEntity.Collider.bounds.extents.x, physicsEntity.transform.position.y);
        }

    }
}
