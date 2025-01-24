using System;
using UnityEngine;

public class PoolAbleObject : MonoBehaviour
{
    private Action<PoolAbleObject> releasePool;
    private Transform poolParent;

    public virtual void InitPoolObject(Action<PoolAbleObject> callback)
    {
        releasePool = callback;
    }

    public void Activate(Transform poolParent)
    {
        this.poolParent = poolParent;
        gameObject.SetActive(true);
    }

    public void Deactivate()
    {
        if (poolParent != null) transform.SetParent(poolParent);
        gameObject.SetActive(false);
    }

    void OnDisable()
    {
        releasePool?.Invoke(this);
        releasePool = null;
    }
}