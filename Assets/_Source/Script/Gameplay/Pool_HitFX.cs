using UnityEngine;
using System.Collections.Generic;

public class Pool_HitFX : MonoBehaviour
{
    [SerializeField] private PoolAbleObject hitFxPrefab;
    [SerializeField] private Transform poolContainer;
    [SerializeField] private List<GameObjectPool> objectPools;

    private void Start()
    {
        objectPools = new List<GameObjectPool>();
    }

    private void OnEnable()
    {
        GameEvents.SpawnHitFx.AddListener(Listener_SpawnHitFX);
    }

    private void OnDisable()
    {
        GameEvents.SpawnHitFx.RemoveListener(Listener_SpawnHitFX);
    }

    private void Listener_SpawnHitFX(    Vector3 spawnPos)
    {
        if (objectPools == null) objectPools = new List<GameObjectPool>();
        var targetPool = objectPools.Find(pool => pool.objectPrefab == hitFxPrefab);

        // Debug.Log($"{name} Spawn Effects  [{prefab.name}]  ");
        if (targetPool == null)
        {
            var poolParent = new GameObject();
            poolParent.name = "Pool -" + hitFxPrefab.name;
            SetParentOrigin(poolParent.transform, poolContainer);
            var newObjectPool = new GameObjectPool();
            newObjectPool.Initialize(poolParent.transform, hitFxPrefab);
            objectPools.Add(newObjectPool);
            targetPool = newObjectPool;
        }

        var newSpawn = targetPool.pool.Get();
        newSpawn.transform.position = spawnPos;

 
        
        newSpawn.Activate(targetPool.poolParent);
    }

    public void SetParentOrigin(Transform self, Transform parent)
    {
        self.SetParent(parent);
        self.localPosition = Vector3.zero;
        self.localRotation = Quaternion.identity;
    }
}
