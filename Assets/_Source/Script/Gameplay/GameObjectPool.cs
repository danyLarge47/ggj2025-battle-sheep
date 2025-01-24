using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class GameObjectPool
{
    public PoolAbleObject objectPrefab;
    public ObjectPool<PoolAbleObject> pool;
    public Transform poolParent;
    private List<GameObject> activeObjects;

    public void Initialize(Transform parent, PoolAbleObject prefab, int defaultCapacity = 20, int maxSize = 100)
    {
        objectPrefab = prefab;
        poolParent = parent;
        activeObjects = new List<GameObject>();
        pool = new ObjectPool<PoolAbleObject>(OnCreatePool, OnGetPool, OnReleasePool, OnDestroyPool, false,
            defaultCapacity, maxSize);
    }

    PoolAbleObject OnCreatePool()
    {
        // Debug.Log($"On Create");
        return GameObject.Instantiate(objectPrefab);
    }

    void OnGetPool(PoolAbleObject poolObject)
    {
        // Debug.Log($"On Get {targetSpawnPoint}");
        poolObject.transform.SetParent(poolParent);
        activeObjects.Add(poolObject.gameObject);

        // poolObject.gameObject.SetActive(true);
        poolObject.InitPoolObject(ReleasePool);
    }

    void OnReleasePool(PoolAbleObject poolObject)
    {
        // Debug.Log($"On Release");
        poolObject.gameObject.SetActive(false);
        activeObjects.Remove(poolObject.gameObject);
    }

    void ReleasePool(PoolAbleObject poolObject)
    {
        // Debug.Log($" ReleaseBullet");
        pool.Release(poolObject);
    }

    void OnDestroyPool(PoolAbleObject poolObject)
    {
        // Debug.Log($"On Destroy");
        GameObject.Destroy(poolObject.gameObject);
    }

    public void SpawnObjectPool(Vector3 spawnPoint, Quaternion rotation)
    {
        var newSpawn = pool.Get();
        newSpawn.transform.position = spawnPoint;
        newSpawn.transform.rotation = rotation;
    }

    public void DisableAllPooledObjects()
    {
        if (activeObjects == null) return;
        // Debug.Log($"DisableAllPooledObjects");
        for (int i = 0; i < activeObjects.Count; i++)
        {
            Debug.Log($"Disable {activeObjects[i]}" , activeObjects[i]);
            activeObjects[i].SetActive(false);
        }
    }
}