using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxPoolComponent : MonoBehaviour
{
    List<BoxCollider2D> m_BoxPool;
    [SerializeField] int m_PoolSize;
    GameObject BoxPoolObject;

    private void Awake()
    {
        m_BoxPool = new List<BoxCollider2D>();
        BoxPoolObject = new GameObject();
        BoxPoolObject.name = "BoxPool";
        BoxPoolObject.transform.position = transform.position;
        BoxPoolObject.transform.parent = transform;

        AddBoxesToPool(m_PoolSize);
    }

    public void ActivateBox(StrikeBox strikeBox)
    {
        GetUnusedBox().GetComponent<HitManager>().Activate(strikeBox);
    }

    BoxCollider2D GetUnusedBox()
    {
        for (int i = 0; i < m_BoxPool.Count; i++)
        {
            if (m_BoxPool[i].enabled == false)
                return m_BoxPool[i];
        }

        AddBoxesToPool(m_PoolSize);

        return GetUnusedBox();
    }

    void AddBoxesToPool(int amount)
    {
        if (amount <= 0)
            amount = 1;

        for (int i = 0; i < amount; i++)
        {
            GameObject box = new GameObject();
            box.layer = 12;
            box.name = "Box"+i;
            box.transform.position = BoxPoolObject.transform.position;
            box.transform.parent = BoxPoolObject.transform;
            
            BoxCollider2D BoxCollider = box.AddComponent<BoxCollider2D>();
            BoxCollider.isTrigger = true;
            m_BoxPool.Add(BoxCollider);
            BoxCollider.enabled = false;

            box.AddComponent<StrikeBoxComponent>();
            box.AddComponent<HitManager>();
        }
    }
}

